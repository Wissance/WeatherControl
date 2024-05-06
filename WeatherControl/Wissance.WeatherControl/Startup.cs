using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Wissance.WeatherControl.Data;
using Wissance.WeatherControl.Data.Extensions;
using Wissance.WeatherControl.WebApi.Config;
using Wissance.WeatherControl.WebApi.Managers;

namespace Wissance.WeatherControl.WebApi
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

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
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
            
            services.AddControllers();

            ConfigureManagers(services);
        }

        private void ConfigureManagers(IServiceCollection services)
        {
            services.AddScoped<StationManager>();
            services.AddScoped<MeasurementsManager>();
        }

        public ApplicationSettings Settings { get; set; }
        private IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        private const string AppName = "Wissance.WeatherControl";
    }
}
