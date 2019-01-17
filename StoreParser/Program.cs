using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StoreParser.Models;
using StoreParser.Parser;
using StoreParser.Parser.ProDJShopParser;
using Microsoft.AspNetCore.Http;
using System.Threading;
using StoreParser.Parser.Interfaces;

namespace StoreParser
{
    public class Program
    {

        public static void Main(string[] args)
        {
            ////parser
            //List<string> strings = new List<string>();

            //ProDjShopParser parser = new ProDjShopParser();
            //ProDjShopParserSettings parserSettings = new ProDjShopParserSettings(1, 3);

            //ParserWorker<string[]> worker = new ParserWorker<string[]>(parser, parserSettings);
            //worker.Settings = parserSettings;
            //worker.Start();
            //worker.OnNewData += (t, x) => strings.AddRange(x);

            /////parser

            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<StoreContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Trace))
                .UseStartup<Startup>();
    }
}
