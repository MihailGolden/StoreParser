using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreParser.Services.TimerBackgroundWorker;

namespace StoreParser.Controllers
{

    //todo: debug cancellation token bug, when Admin action starts. 
    public class AdminController : Controller
    {
        TimedHostedService timedHostedService;

        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token;

        public AdminController(TimedHostedService ths)
        {
            timedHostedService = ths;
            token = cts.Token;

        }

        public ActionResult ProDjShopPaser()
        {
            ViewData["TimerStatus"] = timedHostedService.timerState;
            return View();
        }

        public async Task<ActionResult> StartParse()
        {
            await timedHostedService.StartAsync(token);
            ViewData["TimerStatus"] = timedHostedService.timerState;
            return View("ProDjShopPaser");
        }

        public async Task<ActionResult> StopParse()
        {
            cts.Cancel();
            await timedHostedService.StopAsync(token);
            ViewData["TimerStatus"] = timedHostedService.timerState;
            return View("ProDjShopPaser");
        }
    }
}