﻿using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using StoreParser.Models;
using StoreParser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
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

            //var result = collector.Collect(document);


            string header = null;
            decimal price = 0;
            string url = null;
            string description = null;

            header = document.QuerySelectorAll(settings.HeaderPattern).FirstOrDefault().TextContent.ToString();
            var priceRaw = document.QuerySelectorAll(settings.PricePattern).FirstOrDefault();
            bool isPrice = Decimal.TryParse((priceRaw.Attributes[settings.PriceAttributeKey].Value), out price);
            url = (document.QuerySelectorAll(settings.Url).FirstOrDefault()).Attributes["href"].Value;
            description = document.QuerySelectorAll(settings.DescriptionPattern).FirstOrDefault().TextContent;

            if (true /*!String.IsNullOrWhiteSpace(header) && price > 0 && !String.IsNullOrWhiteSpace(url)*/)
            {
                Product product = new Product()
                {
                    Url = url,
                    Name = header,
                    Description = description,
                    Prices = new List<Price>()
                    {
                        new Price() { ProductPrice = price, PriceLastDate = DateTime.Now }
                    }
                };
                return product;
            }
            return null;
        }
    }
}
