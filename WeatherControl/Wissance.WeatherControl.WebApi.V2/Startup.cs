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
            ILoggerFactory loggerFactory = services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
            // todo(UMV): Resolve security settings using Project Name: that is why important to have same project name for all Backend developers
            // use ~AppData\Local\EdgeDB\config\credentials
            string connStr = GetEdgeDbConnStrByProjectName(Settings.Database.ProjectName, loggerFactory);
            EdgeDBConnection conn = EdgeDBConnection.FromDSN(connStr);
            conn.TLSSecurity = TLSSecurityMode.Insecure;

            services.AddEdgeDB(conn, cfg =>
            {
                cfg.ClientType = EdgeDBClientType.Tcp;
                cfg.SchemaNamingStrategy = INamingStrategy.CamelCaseNamingStrategy;
                cfg.DefaultPoolSize = 256;
                cfg.ConnectionTimeout = 5000;
                cfg.MessageTimeout = 10000;
            });
        }

        private void ConfigureWebApi(IServiceCollection services)
        {
            services.AddControllers();
        }

        /*private void ConfigureManagers(IServiceCollection services)
        {
            // services.AddScoped<StationManager>();
            // services.AddScoped<MeasurementsManager>();
        }*/
        
        private string GetEdgeDbConnStrByProjectName(string projectName, ILoggerFactory loggerFactory)
        {
            ILogger<Startup> logger = loggerFactory.CreateLogger<Startup>();
            try
            {
                string projectCredentialsFile = GetEdgeDbProjectCredentialFile(projectName);
                string content = File.ReadAllText(projectCredentialsFile);
                EdgeDbProjectCredentials credentials = JsonConvert.DeserializeObject<EdgeDbProjectCredentials>(content);
                //JsonSerializer.Deserialize<EdgeDbProjectCredentials>(content);
                return string.Format(EdgeDbConnStrTemplate, credentials.User, credentials.Password, "localhost",
                    credentials.Port,
                    credentials.Database);
            }
            catch (Exception e)
            {
                logger.LogError($"An error occurred during attempt to build edgedb connection str: {e.Message}");
                return string.Empty;
            }
        }

        private string GetEdgeDbProjectCredentialFile(string projectName)
        {
            string projectCredentialsFile = string.Empty;
            if (OperatingSystem.IsWindows())
            {
                // should be using ~AppData\Local\EdgeDB\config\credentials\{projectName}.json as a path
                string appData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
                projectCredentialsFile = Path.Combine(new string[]
                {
                    appData, 
                    "EdgeDB", "config", "credentials", 
                    projectName + ".json"
                });
            }

            if (OperatingSystem.IsLinux())
            {
                // linux - /.config/edgedb/credentials/{projectName}.json relative to home dir
                string home = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                projectCredentialsFile = Path.Combine(new string[]
                {
                    home, 
                    ".config", "edgedb", "credentials", 
                    projectName + ".json"
                });
            }

            return projectCredentialsFile;
        }

        public ApplicationSettings Settings { get; set; }
        private IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        
        private const string EdgeDbConnStrTemplate = "edgedb://{0}:{1}@{2}:{3}/{4}";
    }
}
