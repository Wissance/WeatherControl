using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog;
using EdgeDB;
using Microsoft.AspNetCore.Routing.Internal;
using Newtonsoft.Json;
using Wissance.EdgeDb.Configurator;
using Wissance.WeatherControl.WebApi.V2.Config;
using Wissance.WeatherControl.WebApi.V2.Data;


namespace Wissance.WeatherControl.WebApi.V2
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
            ConfigureCulture();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void ConfigureCulture()
        {
            CultureInfo cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
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
            EdgeDBClientPoolConfig poolCfg = new EdgeDBClientPoolConfig()
            {
                ClientType = EdgeDBClientType.Tcp,
                SchemaNamingStrategy = INamingStrategy.CamelCaseNamingStrategy,
                DefaultPoolSize = 256,
                ConnectionTimeout = 5000,
                MessageTimeout = 10000
            };
            services.ConfigureEdgeDbDatabase("192.168.119,128", Settings.Database.ProjectName, poolCfg,
                new []{"."}, true);
            // services.ConfigureEdgeDbDatabase();
        }

        private void ConfigureWebApi(IServiceCollection services)
        {
            services.AddControllers();
        }

        public ApplicationSettings Settings { get; set; }
        private IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        
        private const string EdgeDbConnStrTemplate = "edgedb://{0}:{1}@{2}:{3}/{4}";
    }
}
