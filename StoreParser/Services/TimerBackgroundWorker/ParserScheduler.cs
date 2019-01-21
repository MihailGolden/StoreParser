using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StoreParser.Models;
using StoreParser.Parser;
using StoreParser.Parser.ProDjShopUrlCollector;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace StoreParser.Services.TimerBackgroundWorker
{
    //
    // Summary:
    //     Timer for running background task.
    public class ParserScheduler : IHostedService
    {
        //
        // Summary:
        //     Get or set actual timer state.
        public string timerState { get; set; } = "stopped";
        //
        // Summary:
        //     Get or set time interval for doig task.
        public int period { get; set; } = 60*60*24; //seconds = per a day

        private StoreContext db;

        private readonly ILogger _logger;
        private Timer _timer;
        private int _counter;
        private readonly CancellationTokenSource _stoppingCts =
                                                   new CancellationTokenSource();

        public ParserScheduler(ILogger<ParserScheduler> logger, StoreContext dbContext)
        {
            _counter = 0;
            _logger = logger;
            _stoppingCts.Cancel();
            db = dbContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _logger.LogDebug("Timed Background Service is starting.");

            _timer = new Timer(DoWork, cancellationToken, TimeSpan.Zero,
                TimeSpan.FromSeconds(period));
            timerState = "running";
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            ProDjShopUrlCollector collector = new ProDjShopUrlCollector();
            List<string> strings = new List<string>();
            
            ProDjShopUrlCollectorSettings collectorSettings = new ProDjShopUrlCollectorSettings();
            UrlCollectorWorker<string[]> worker = new UrlCollectorWorker<string[]>(collector, collectorSettings);
            worker.Settings = collectorSettings;
            worker.Start();
            ProductParserWorker productParserWorker = new ProductParserWorker(db);
            worker.OnNewData += productParserWorker.DoWork;

            _logger.LogDebug("Timed Background Service is working." + (_counter++.ToString()));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogDebug("Timed Background Service is stopping.");

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
