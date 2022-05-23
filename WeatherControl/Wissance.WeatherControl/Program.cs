using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Wissance.WeatherControl.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHostBuilder webHostBuilder = CreateWebHostBuilder(args);
            IWebHost webHost = webHostBuilder.Build();
            webHost.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            IWebHostBuilder webHostBuilder = Microsoft.AspNetCore.WebHost.CreateDefaultBuilder();
            _environment = webHostBuilder.GetSetting("environment");
            // todo: umv: read configuration here
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{_environment}.json")
                .Build();
            webHostBuilder.UseStartup<Startup>()
                .UseConfiguration(configuration);
            return webHostBuilder;
        }

        private static string _environment;
    }
}
