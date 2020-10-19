using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MM.Common.Utils;

namespace MM.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Added: CORS see https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1
            //note: add nuget package Microsoft.AspNetCore.Cors


            /*
            //Add default CORS policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder
                      .AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
                    });
            });
            */

            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    // Not a permanent solution, but just trying to isolate the problem
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });




            #endregion

            services.AddMvc();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            #region Added: CORS see https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1
            //note: add nuget package Microsoft.AspNetCore.Cors

            //TO use specific polcy
            app.UseCors("AllowAll");
            //TO use default policy
            //app.UseCors();

            #endregion


            app.UseAuthorization();


            #region Added: default  page Celebs\Index

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Celebs}/{action=Index}/{id?}");


            });


            #endregion

            #region Added:  static files support (eg, js)

            //see https://docs.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-3.1  to allow script src="~/JavaScript/jquery-1.12.4.min.js"

            app.UseStaticFiles();

            #endregion

            #region Added: Logging support (with IOC)

            //see: https://www.c-sharpcorner.com/article/add-file-logging-to-an-asp-net-core-mvc-application/
            //     https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1
            //note: add nuget package    Serilog.Extensions.Logging.File   
            //note: appsettings.json  and alos appsettings.Development.json  contains logging section to define which trace level are written to log see: https://github.com/NLog/NLog.Web/wiki/Missing-trace%5Cdebug-logs-in-ASP.NET-Core-3%3F
            //usage:
            //  controller data memeber:        private readonly ILogger<HomeController> _logger;
            //  contorller constructor:         public HomeController(ILogger<HomeController> logger) {   _logger = logger;}
            //  contorller action               _logger.LogInformation("The main page has been accessed");  


            //var path = Directory.GetCurrentDirectory();
            //loggerFactory.AddFile($"{path}\\Logs\\Log.txt");
            LogManager.init_LogFactory(loggerFactory);

            #endregion


        }
    }
}
