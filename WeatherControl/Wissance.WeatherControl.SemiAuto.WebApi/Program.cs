using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Wissance.WeatherControl.SemiAuto.WebApi
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