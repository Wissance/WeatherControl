using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Wissance.WeatherControl.WebApi.V2;
using Wissance.WeatherControl.WebApi.V2.Config;

//using Wissance.WeatherControl.Ef.Data;
//using Wissance.WeatherControl.Ef.Data.Extensions;
//using Wissance.WeatherControl.WebApi.Config;
//using Wissance.WeatherControl.WebApi.Managers;

namespace Wissance.WeatherControl.WebApi.V2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHostBuilder hostBuilder = CreateWebHostBuilder(args);
            hostBuilder.Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args)
        {
            //todo: umv: temporarily stub
            _environment = "Development";
            //webHostBuilder.GetSetting("environment");

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{_environment}.json")
                .Build();
            IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(builder =>
            {
                builder.UseConfiguration(configuration);
                builder.UseStartup<Startup>();
                builder.UseKestrel();
            });
            return hostBuilder;
        }

        private static string _environment;
    }
}
