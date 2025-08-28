using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Wissance.WeatherControl.Data;
using Wissance.WeatherControl.Data.Extensions;
using Wissance.WeatherControl.Common.Config;
using Wissance.WeatherControl.Data.Entity;
using Wissance.WebApiToolkit.Core.Data;
using Wissance.WebApiToolkit.Core.Managers;
using Wissance.WebApiToolkit.Ef.Factories;
using Wissance.WebApiToolkit.Ef.Managers;
using Wissance.WebApiToolkit.Ef.Extensions;
using Wissance.WebApiToolkit.Ef.Generators;

namespace Wissance.WeatherControl.SemiAuto.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
            Settings = configuration.GetSection("Application").Get<ApplicationSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureLogging(services);
            ConfigureDatabase(services);
            ConfigureWebApi(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", AppName);
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureLogging(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();
            services.AddLogging(loggingBuilder => loggingBuilder.AddConfiguration(Configuration).AddConsole());
            services.AddLogging(loggingBuilder => loggingBuilder.AddConfiguration(Configuration).AddDebug());
            services.AddLogging(loggingBuilder => loggingBuilder.AddConfiguration(Configuration).AddSerilog(dispose: true));
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.ConfigureSqlServerDbContext<ModelContext>(Settings.Database.ConnStr);
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ModelContext modelContext = serviceProvider.GetRequiredService<ModelContext>();
            modelContext.Database.Migrate();
        }

        private void ConfigureWebApi(IServiceCollection services)
        {
            services.AddSwaggerGen();
            ServiceProvider provider = services.BuildServiceProvider();
            Assembly stationControllerAssembly = services.AddSimplifiedAutoController<StationEntity, Guid, EmptyAdditionalFilters>(
                provider.GetRequiredService<ModelContext>(), "Station",
                ControllerType.FullCrud, null, provider.GetRequiredService<ILoggerFactory>());
            Assembly measureUnitControllerAssembly = services.AddSimplifiedAutoController<MeasureUnitEntity, Guid, EmptyAdditionalFilters>(
                provider.GetRequiredService<ModelContext>(), "MeasureUnit",
                ControllerType.ReadOnly, null, provider.GetRequiredService<ILoggerFactory>());
            Assembly sensorControllerAssembly = services.AddSimplifiedAutoController<SensorEntity, Guid, EmptyAdditionalFilters>(
                provider.GetRequiredService<ModelContext>(), "Sensor",
                ControllerType.FullCrud, null, provider.GetRequiredService<ILoggerFactory>());
            Assembly measurementControllerAssembly = services.AddSimplifiedAutoController<MeasurementEntity, Guid, EmptyAdditionalFilters>(
                provider.GetRequiredService<ModelContext>(), "Measurement",
                ControllerType.Bulk, null, provider.GetRequiredService<ILoggerFactory>());
            
            services.AddControllers().AddApplicationPart(sensorControllerAssembly).AddControllersAsServices();
            services.AddControllers().AddApplicationPart(stationControllerAssembly).AddControllersAsServices();
            services.AddControllers().AddApplicationPart(measureUnitControllerAssembly).AddControllersAsServices();
            services.AddControllers().AddApplicationPart(measurementControllerAssembly).AddControllersAsServices();
        }

        private void ConfigureManagers(IServiceCollection services)
        {
            services.AddScoped(sp =>
            {
                return SimplifiedEfBasedManagerFactory.Create<StationEntity, Guid>(
                    sp.GetRequiredService<ModelContext>(),
                    null, new LoggerFactory());

            });

            /*services.AddKeyedTransient("StationController", (sp, key) =>
            {
                return new StationController(sp.GetRequiredService<IModelManager<StationEntity, StationEntity, Guid>>());
            });*/
            //services.AddScoped<SensorManager>();
            //services.AddScoped<MeasureUnitManager>();
            //services.AddScoped<MeasurementManager>();
        }

        /*private Assembly ConfigureAutoControllers(IServiceCollection services)
        {
            try
            {
                //Assembly controllersAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("AutoControllers"), 
                //    AssemblyBuilderAccess.Run);
                AssemblyName assemblyName = new AssemblyName("AutoGeneratedControllers");
                // temporarily used Persist
                
                AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
                
                ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("AutoGeneratedControllers");
                TypeBuilder typeBuilder = moduleBuilder.DefineType("StationController",
                    TypeAttributes.Public | TypeAttributes.Class,
                    typeof(GenericController<StationEntity, StationEntity, Guid, EmptyAdditionalFilters>));

                // todo(umv): add constructor
                // typeBuilder.DefineGenericParameters();
                ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(
                    MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName,
                    CallingConventions.Standard,
                    new Type[] {typeof(IModelManager<StationEntity, StationEntity, Guid>)});
                constructorBuilder.DefineParameter(1, ParameterAttributes.In, "manager");

                ILGenerator ilGenerator = constructorBuilder.GetILGenerator();
                
                ilGenerator.Emit(OpCodes.Ret); // Return from the constructor
                Type t = typeBuilder.CreateType();
                Assembly ass = Assembly.GetAssembly(t);
                return ass;
                
            }
            catch (Exception e)
            {
                return null;
            }
        }*/
        
        public ApplicationSettings Settings { get; set; }
        private IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        private const string AppName = "Wissance.WeatherControl";
    }
}
