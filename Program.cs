using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace hotelaanbieding_be
{
    // Hotel Management API Program.. 
    public class Program
    {
        internal const string RoleName = "Hotel Management API";
        public static void Main(string[] args)
        {
            Console.Title = RoleName;

            try
            {
                StartApi(args);
            }
            catch (Exception ex)
            {
                //logg exception
            }

        }

        private static void StartApi(string[] args, int retryCount = 0)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex) when (retryCount < 3)
            {
                ++retryCount;

                //Log Exception
                //also use thread sleep for each retry 5 and 10 secs

                StartApi(args, retryCount);
            }

        }
        public static IConfiguration Config { get; private set; }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webbuilder =>
                {
                    webbuilder.UseKestrel()
                                .UseStartup<Startup>();
                }).ConfigureAppConfiguration(
                (context, config) =>
                {

                    config.SetBasePath(Path.GetDirectoryName(typeof(Program).Assembly.Location));

                    if (context.HostingEnvironment.IsDevelopment())
                    {
                        config.AddJsonFile("appSettings.Local.json", true, reloadOnChange: true);
                    }
                    else
                    {
                        // Read values from KeyVault
                    }

                    Config = config.Build();

                    foreach (KeyValuePair<string, string> item in Config.AsEnumerable())
                    {
                        ConfigurationManager.AppSettings.Set(item.Key, item.Value);
                    }

                }).ConfigureLogging(
                (ctx, logging) =>
                {
                    //laod log configurations or AppInsights
                });
    }
}
