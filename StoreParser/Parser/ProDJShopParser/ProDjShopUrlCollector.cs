using AngleSharp.Dom.Html;
using StoreParser.Parser.Interfaces;
using System.Collections.Generic;

namespace StoreParser.Parser.ProDJShopParser
{
    public class ProDjShopUrlCollector : IUrlCollector<string[]>
    {
        public string[] Collect(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("span.bt-name a");

            foreach (var item in items)
            {
                list.Add(item.Attributes["href"].Value);
            }

            return list.ToArray();
        }
    }
}
