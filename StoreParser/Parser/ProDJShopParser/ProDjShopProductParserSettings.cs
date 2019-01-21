using StoreParser.Parser.Interfaces;

namespace StoreParser.Parser.ProDJShopParser
{
    public class ProDjShopProductParserSettings : IProductParserSettings
    {
        public string PricePattern { get; set; } = "div.tov-price.data-cart";
        public string PriceAttributeKey { get; set; } = "data-p";
        public string HeaderPattern { get; set; } = "div.tov-h1 h1";
        public string DescriptionPattern { get; set; } = "div.tov-text1.lnk1";
        public string ImagePattern { get; set; } = "div.tov-gallery.mobile-invis>a";
        public string Url { get; set; } = "link[rel=canonical]";
        public string Prefix { get; set; } = "https://www.prodj.com.ua";
    }
}
