﻿using AngleSharp.Dom.Html;
using StoreParser.Models;
using StoreParser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreParser.Parser.ProDJShopParser
{
    public class ProDjShopProductParser : IProductParser<Product>
    {
        readonly HttpClient client;
        readonly string url;


        public Product Parse(IHtmlDocument document)
        {
            throw new NotImplementedException();
        }
    }
}