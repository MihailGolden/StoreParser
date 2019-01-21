using StoreParser.Models;
using StoreParser.Parser.ProDjShopUrlCollector;
using System.Collections.Generic;

namespace StoreParser.Parser
{
    public class ProductParserWorker
    {

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
                string source = await productLoader.LoadAsync(settings.Prefix + url);
                Product product = await parser.Parse(source);

                if (product.Name != null)
                {
                    productList.Add(product);
                }
            }
            await saver.SaveProducts(productList);
        }
    }
}
