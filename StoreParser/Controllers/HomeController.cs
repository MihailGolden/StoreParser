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

        public JsonResult Index()
        {
            List<string> results;

# region comments

            //Price price1 = new Price { ProductPrice = 666m, PriceLastDate = DateTime.Now };
            //Price price2 = new Price { ProductPrice = 664m, PriceLastDate = DateTime.Now };
            //Product product = new Product { Name = "Lolol", Prices = new List<Price> { price1, price2 } };

            //var res = db.Products.Include(p => p.Prices);
            //Price price = new Price();
            //Product prod = new Product();

            //foreach (var item in res)
            //{
            //    prod = item;
            //    foreach (var item2 in item.Prices)
            //    {
            //        price = item2;
            //        prod.Prices.Add(price);
            //    }
            //}

            //string s = res.Name;
            //foreach (var price in res.Prices)
            //{
            //    s += price.ProductPrice;
            //}




            //var prods = db.Products.ToList();

            //var records = from entity in prods
            //              select new
            //              {
            //                  name = entity.Name,
            //                  id = entity.Id,
            //                  prices = 
            //                            from p in entity.Prices
            //                            select new
            //                            {
            //                                price = p.ProductPrice,
            //                                date = p.PriceLastDate
            //                            }
            //              };


            //-------///
# endregion 

            var records = db.Products.Select(p => new
            {
                Name = p.Name,
                Id = p.Id,
                Prices = p.Prices.Select(pr => new
                {
                    Price = pr.ProductPrice,
                    Date = pr.PriceLastDate
                })
            });

            return Json(records);
            ///-------////
        }

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
