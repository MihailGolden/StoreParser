using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StoreParser.Models;
using StoreParser.Parser;
using StoreParser.Parser.Interfaces;
using StoreParser.Parser.ProDJShopParser;
using StoreParser.Services.TimerBackgroundWorker;

namespace StoreParser
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
            //background timer for parsing
            services.AddSingleton<TimedHostedService>();


            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(connection));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddSingleton<IParser<string[]>, ProDjShopParser>();

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(TimedHostedService timer, IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory /*IParser<string[]> parser, */)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(async (context) =>
            {
                //timer block
                //CancellationTokenSource cts = new CancellationTokenSource();
                //var token = cts.Token;
                //timer.StartAsync(token);


                var logger = loggerFactory.CreateLogger("RequestInfoLogger");

                ProDjShopParser parser = new ProDjShopParser();
                List<string> strings = new List<string>();
                ProDjShopParserSettings parserSettings = new ProDjShopParserSettings(1, 3);
                ParserWorker<string[]> worker = new ParserWorker<string[]>(parser, parserSettings);
                worker.Settings = parserSettings;
                worker.Start();
                worker.OnNewData += async (t, x) =>
                {
                    foreach (var header in x)
                    {
                        logger.LogError(header);
                    }
                    //strings.AddRange(x);
                    //await context.Response.WriteAsync(x.Count().ToString());
                };
                await context.Response.WriteAsync("ok");
            });
        }
    }
}
