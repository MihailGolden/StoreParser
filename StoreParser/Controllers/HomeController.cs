using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreParser.Models;
using Microsoft.EntityFrameworkCore;
using StoreParser.Parser;
using StoreParser.Parser.ProDjShopUrlCollector;
using AngleSharp.Parser.Html;
using System.Globalization;

namespace StoreParser.Controllers
{
    public class HomeController : Controller
    {
        StoreContext db;
        UrlCollectorWorker<string[]> collector;

        public HomeController(StoreContext context)
        {
            db = context;
        }

        //public async Task<ActionResult> Index()
        //{
        //    var products = db.Products.ToList();
        //    return View(products);
        //}

        public async Task<JsonResult> Index()
        {
            //ProductLoader pl = new ProductLoader();
            //ProDjShopProductParser parser = new ProDjShopProductParser(new ProDjShopProductParserSettings());
            //string url = "https://www.prodj.com.ua/studio-monitors/monkey-banana-turbo-4-black";
            //string source = await pl.Load(url);
            //Product product = await parser.Parse(source);
            //var domParser = new HtmlParser();

            //var document = await domParser.ParseAsync(source);

            //var result = collector.Collect(document);


            var prods = db.Products.ToList();

        var products = from entity in prods
                       select new
                       {
                           name = entity.Name,
                           id = entity.Id,
                           prices =
                                     from p in entity.Prices
                                     select new
                                     {
                                         price = p.ProductPrice,
                                         date = p.PriceLastDate
                                     }
                       };
            return Json(products);
    }

    public string GetCulture(string code = "")
        {
            if (!String.IsNullOrEmpty(code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(code);
                CultureInfo.CurrentUICulture = new CultureInfo(code);
            }
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }
        //        public JsonResult Index()
        //        {
        //            List<string> results;

        //# region comments

        //            //Price price1 = new Price { ProductPrice = 666m, PriceLastDate = DateTime.Now };
        //            //Price price2 = new Price { ProductPrice = 664m, PriceLastDate = DateTime.Now };
        //            //Product product = new Product { Name = "Lolol", Prices = new List<Price> { price1, price2 } };

        //            //var res = db.Products.Include(p => p.Prices);
        //            //Price price = new Price();
        //            //Product prod = new Product();

        //            //foreach (var item in res)
        //            //{
        //            //    prod = item;
        //            //    foreach (var item2 in item.Prices)
        //            //    {
        //            //        price = item2;
        //            //        prod.Prices.Add(price);
        //            //    }
        //            //}

        //            //string s = res.Name;
        //            //foreach (var price in res.Prices)
        //            //{
        //            //    s += price.ProductPrice;
        //            //}




        //            //var prods = db.Products.ToList();

        //            //var records = from entity in prods
        //            //              select new
        //            //              {
        //            //                  name = entity.Name,
        //            //                  id = entity.Id,
        //            //                  prices = 
        //            //                            from p in entity.Prices
        //            //                            select new
        //            //                            {
        //            //                                price = p.ProductPrice,
        //            //                                date = p.PriceLastDate
        //            //                            }
        //            //              };


        //            //-------///
        //# endregion 

        //            var records = db.Products.Select(p => new
        //            {
        //                Name = p.Name,
        //                Id = p.Id,
        //                Prices = p.Prices.Select(pr => new
        //                {
        //                    Price = pr.ProductPrice,
        //                    Date = pr.PriceLastDate
        //                })
        //            });

        //            return Json(records);
        //            ///-------////
        //        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
