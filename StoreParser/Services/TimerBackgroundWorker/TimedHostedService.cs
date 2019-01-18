using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoreParser.Models;
using StoreParser.Parser;
using StoreParser.Parser.ProDJShopParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace StoreParser.Services.TimerBackgroundWorker
{
    //
    // Summary:
    //     Timer for running background task.
    public class TimedHostedService : IHostedService
    {
        //
        // Summary:
        //     Get or set actual timer state.
        public string timerState { get; set; } = "stopped";
        //
        // Summary:
        //     Get or set time interval for doig task.
        public int period { get; set; } = 15;

        private StoreContext db;

        private readonly ILogger _logger;
        private Timer _timer;
        private int _counter;
        private readonly CancellationTokenSource _stoppingCts =
                                                   new CancellationTokenSource();

        public TimedHostedService(ILogger<TimedHostedService> logger, StoreContext dbContext)
        {
            _counter = 0;
            _logger = logger;
            _stoppingCts.Cancel();
            db = dbContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, cancellationToken, TimeSpan.Zero,
                TimeSpan.FromSeconds(period));
            timerState = "running";
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            ProDjShopParser parser = new ProDjShopParser();
            List<string> strings = new List<string>();
            ProDjShopParserSettings parserSettings = new ProDjShopParserSettings(1, 3);
            ParserWorker<string[]> worker = new ParserWorker<string[]>(parser, parserSettings);
            worker.Settings = parserSettings;
            worker.Start();
            worker.OnNewData += async (t, x) =>
            {
                int counter = 1;
                foreach (var header in x)
                {
                    db.Links.Add(new Link { Url = header });
                    db.SaveChanges();
                    counter++;
                    _logger.LogInformation(header);

                }

                //strings.AddRange(x);
                //await context.Response.WriteAsync(x.Count().ToString());
            };

            _logger.LogInformation("Timed Background Service is working." + (_counter++.ToString()));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            timerState = "stopped";

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
