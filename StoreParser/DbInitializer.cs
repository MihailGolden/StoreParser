using StoreParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser
{
    public class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "iPhone 6S",
                        Description = "Description",
                        Images = new List<Image>(),
                        Prices = 
                                new List<Price>()
                                {
                                    new Price(){ ProductPrice = 400m, PriceLastDate = DateTime.Now }
                                }
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
