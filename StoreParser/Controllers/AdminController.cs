﻿using Microsoft.AspNetCore.Mvc;
using StoreParser.Services.TimerBackgroundWorker;
using System.Threading;
using System.Threading.Tasks;

namespace StoreParser.Controllers
{
    public class AdminController : Controller
    {
        ParserScheduler parserScheduler;

        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken token;

        public AdminController(ParserScheduler scheduler)
        {
            parserScheduler = scheduler;
            token = cts.Token;

        }

        public ActionResult ProDjShopPaser()
        {
            ViewData["TimerStatus"] = parserScheduler.timerState;
            return View();
        }

        public async Task<ActionResult> StartParse()
        {
            await parserScheduler.StartAsync(token);
            ViewData["TimerStatus"] = parserScheduler.timerState;
            return View("ProDjShopPaser");
        }

        public async Task<ActionResult> StopParse()
        {
            cts.Cancel();
            await parserScheduler.StopAsync(token);
            ViewData["TimerStatus"] = parserScheduler.timerState;
            return View("ProDjShopPaser");
        }
    }
}