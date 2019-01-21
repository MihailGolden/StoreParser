using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreParser.Models;
using StoreParser.Parser.Interfaces;
using StoreParser.Services.TimerBackgroundWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser
{
    public class ProductDbSaver : IProductSaver<Product>
    {
        private StoreContext db;

        public ProductDbSaver(StoreContext dbContext)
        {
            db = dbContext;
        }

        public async Task SaveProducts(List<Product> products)
        {
            foreach (Product product in products)
            {
                var productsDb = db.Products;

                if (db.Products.Any(p => p.Url == product.Url))
                {
                    //price analysing and adding new price
                    var dbProduct = db.Products.Where(p => p.Url == product.Url).FirstOrDefault();
                    var dbProductDates = dbProduct.Prices.Select(d => d.PriceLastDate);
                    var maxdbProductDate = dbProductDates.Max();
                    var lastDbProductPrice = dbProduct.Prices.Where(d => d.PriceLastDate == maxdbProductDate).FirstOrDefault();
                    var priceFromParser = product.Prices.FirstOrDefault();
                    if(priceFromParser.ProductPrice != lastDbProductPrice.ProductPrice)
                    {
                        dbProduct.Prices.Add(priceFromParser);
                        await db.SaveChangesAsync();
                    }
                    continue;
                }
                else
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
            }
        }
    }
}
