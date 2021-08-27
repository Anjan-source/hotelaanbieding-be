using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hotelaanbieding_be.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly.Registry;

namespace hotelaanbieding_be
{
    //configure the service
    public class Startup
    {
        private readonly IConfiguration config;
        private readonly ILogger logger;
        public Startup(IConfiguration configuration, ILogger<Startup> ilogger)
        {
            config = Verify.NotNull(nameof(configuration), configuration) as IConfiguration;
            logger = Verify.NotNull(nameof(ilogger), ilogger) as ILogger<Startup>;
        }

        /// <summary>
        ///     Called by the ASP.NET Core runtime when the service starts for setting
        ///     dependency injection
        /// </summary>
        /// <param name="services">Required not null implementation of IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            Verify.NotNull(nameof(services), services);

            services.AddSingleton(typeof(IReadOnlyPolicyRegistry<string>),
                new PolicyRegistry()
                {
                    //provide key and value with timeout retry
                }
            );

            services.AddHttpClient();

            services.AddMvcCore()
                    .AddAuthorization()
                    .AddApiExplorer()
                    .AddFormatterMappings()
                    .AddCacheTagHelper()
                    .AddDataAnnotations()
                    .AddControllersAsServices();

            services.AddCors(option =>
                {
                    option.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            SetupLifetime(app, logger);

            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }
            // This should be before authorization (so routing information is available to authorization middleware)
            app.UseRouting();
            app.UseCors();
            app.UseCors("CorsPolicy");

            //here we can add Autherization and AppInsight configrations in the middleware

            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void SetupLifetime(IApplicationBuilder app, ILogger<Startup> logger)
        {
            Verify.NotNull(nameof(app), app);
            Verify.NotNull(nameof(logger), logger);

            IHostApplicationLifetime lifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            lifetime.ApplicationStarted.Register(() => logger.LogInformation("Application is starting"));
            lifetime.ApplicationStopping.Register(() => logger.LogInformation("Application is stopping"));
            lifetime.ApplicationStopped.Register(() => logger.LogInformation("Application is stopped"));
        }
    }
}
