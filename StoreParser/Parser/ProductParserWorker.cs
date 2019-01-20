using Microsoft.Extensions.Logging;
using StoreParser.Models;
using StoreParser.Parser.Interfaces;
using StoreParser.Parser.ProDjShopUrlCollector;
using StoreParser.Services.TimerBackgroundWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser
{
    public class ProductParserWorker
    {
        //private IProductParser<Product> parser;
        //private IProductParserSettings parserSettings;


        //ProductParserWorker(IProductParser<Product> parser, IProductParserSettings parserSettings)
        //{
        //    this.parser = parser;
        //    this.parserSettings = parserSettings;
        //}

        private ProductLoader productLoader;
        private ProDjShopProductParserSettings settings;
        private ProDjShopProductParser parser;
        private ProductDbSaver saver;
        private StoreContext db;

        public ProductParserWorker(StoreContext dbContext)
        {
            productLoader = new ProductLoader();
            settings = new ProDjShopProductParserSettings();
            parser = new ProDjShopProductParser(settings);
            db = dbContext;

            saver = new ProductDbSaver(db);
        }

        public async void DoWork(object obj, string[] urls)
        {
            List<Product> productList = new List<Product>();
            foreach (string url in urls)
            {
                string source = await productLoader.Load(settings.Prefix + url);
                var product = await parser.Parse(source);
                if(product != null)
                {
                    productList.Add(product);
                }
            }
            await saver.SaveProducts(productList);
        }
    }
}
