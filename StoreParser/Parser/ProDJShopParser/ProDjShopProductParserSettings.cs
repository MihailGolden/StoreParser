using StoreParser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.ProDJShopParser
{
    public class ProDjShopProductParserSettings : IProductParserSettings
    { 
        //public ProDjShopProductParserSettings()
        //{
        //    //Url = url;
        //    HeaderPattern = headerPattern;
        //    DescriptionPattern = descriptionPattern;
        //    ImagePattern = imagePattern;
        //}
        //public string[] Url { get; set; }
        public string PricePattern { get; set; } = "div.tov-price.data-cart";
        public string PriceAttributeKey { get; set; } = "data-p";
        public string HeaderPattern { get; set; } = "div.tov-h1 h1";
        public string DescriptionPattern { get; set; } = "div.tov-text1.lnk1";
        public string ImagePattern { get; set; } = "img.mfp-img";
    }
}
