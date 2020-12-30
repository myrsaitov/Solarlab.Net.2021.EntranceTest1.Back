using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace WebApi
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
         .AddEnvironmentVariables()
         .Build();

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            
            Log.Logger = new LoggerConfiguration()
             .ReadFrom.Configuration(Configuration)
             .Enrich.WithProperty("App Name", "IT Academy 2020 Team 8")
             .CreateLogger();
            try
            {
                Log.Information("Apps started!");
                CreateHostBuilder(args).Build().Run();
                Log.Information("Apps ended!");
                return;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
