using AngleSharp.Parser.Html;
using StoreParser.Models;
using StoreParser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.ProDjShopUrlCollector
{
    public class ProDjShopProductParser : IProductParser<Product>
    {
        private ProDjShopProductParserSettings settings;
        public ProDjShopProductParser(ProDjShopProductParserSettings settings)
        {
            this.settings = settings;
        }

        public async Task<Product> Parse(string source)
        {
            var domParser = new HtmlParser();

            var document = await domParser.ParseAsync(source);
         
            string header = await Task.Run(() => document.QuerySelectorAll(settings.HeaderPattern).FirstOrDefault().TextContent.ToString());
            string priceRaw = await Task.Run(() => (document.QuerySelectorAll(settings.PricePattern)?.FirstOrDefault()?.Attributes[settings.PriceAttributeKey]?.Value));
            decimal price = priceRaw != null ? Decimal.Parse(priceRaw): 0m;
            string url = await Task.Run(() => (document.QuerySelectorAll(settings.Url).FirstOrDefault()).Attributes["href"].Value);
            string description = await Task.Run(() => document.QuerySelectorAll(settings.DescriptionPattern).FirstOrDefault().TextContent);
            description = description.Trim().Replace('\n', ' ');

            string[] imagesUrls = await Task.Run(() => document.QuerySelectorAll(settings.ImagePattern).Select(i => i.Attributes["href"].Value).ToArray());

            ImageDownloader imageDownloader = new ImageDownloader();

            List<Image> imageList = new List<Image>();
            foreach (string imagesUrl in imagesUrls)
            {
                Image image = await imageDownloader.DownloadImageAsync(imagesUrl);
                imageList.Add(image);
            }

            if (!String.IsNullOrWhiteSpace(header) && price > 0 && !String.IsNullOrWhiteSpace(url))
            {
                Product product = new Product()
                {
                    Url = url,
                    Name = header,
                    Description = description,
                    Images = imageList,
                    Prices = new List<Price>()
                    {
                        new Price() { ProductPrice = price, PriceLastDate = DateTime.Now }
                    }
                };
                return  product;
            }
            return new Product();
        }
    }
}
