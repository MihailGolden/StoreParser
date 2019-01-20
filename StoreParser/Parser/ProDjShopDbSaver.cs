using Microsoft.EntityFrameworkCore;
using StoreParser.Models;
using StoreParser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser
{
    public class ProDjShopDbSaver : IProductSaver<Product>
    {

        private StoreContext db;

        public ProDjShopDbSaver(StoreContext dbContext)
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
                    continue;
                }
                else
                {
                    db.Products.Add(product);
                }
                db.SaveChanges();
            }
        }
    }
}
