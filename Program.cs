﻿using System;
using System.Globalization;
using core.configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using scheduler;

namespace ConsoleApplication
{
    public class Program
    {
        public static JsonConfiguration jsonConf;

          public static ILoggerFactory loggerFactory;
               // This method gets called by the runtime. Use this method to add services to the container.


          public void Configure(IApplicationBuilder app)
          {
            // ici nous allons ouvrire le fichier de configuration
           
            try{
               jsonConf = new JsonConfiguration();
              
            }catch(Exception exc){
                Console.WriteLine(exc.Message);
            }
            
            app.UseMvcWithDefaultRoute();
             // ********************
            // USE CORS - might not be required.
            // ********************
            app.UseCors("SiteCorsPolicy");
            
         }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
             // ********************
            // Setup CORS
            // ********************
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        

        public static void Main(string[] args)
        {
            /*
            string date = DateTime.Today.AddDays(1).ToString("MM/dd/yy",CultureInfo.InvariantCulture);
            string cmd = $"script.sh {date}";
            Console.WriteLine(cmd);
            Schedule schedule = new Schedule(cmd);
            schedule.executeTask();   
            */
                var host = new WebHostBuilder()
                            .UseKestrel()
                            //.UseUrls("http://192.168.1.31:5000")
                            .UseStartup<Startup>()
                            .Build();
                host.Run();

           
        
        }
        
    }
}
