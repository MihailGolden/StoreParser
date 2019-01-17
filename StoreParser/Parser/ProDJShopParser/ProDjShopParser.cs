using AngleSharp.Dom.Html;
using StoreParser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.ProDJShopParser
{
    class ProDjShopParser : IParser<string[]>
    {
        //IParser<string[]> _parser;

        //public ProDjShopParser(IParser<string[]> parser)
        //{
        //    _parser = parser;
        //}

        public string[] Parse(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("div.block-tov a");
                //.Where(item => item.ClassName != null && item.ClassName.Contains("block-tov"));

            foreach (var item in items)
            {
                list.Add(item.TextContent);
            }

            return list.ToArray();
        }
    }
}
