using AngleSharp.Dom.Html;
using StoreParser.Parser.Interfaces;
using System.Collections.Generic;

namespace StoreParser.Parser.ProDjShopUrlCollector
{
    class ProDjShopUrlCollector : IUrlCollector<string[]>
    {
        public string[] Collect(IHtmlDocument document)
        {
            var list = new List<string>();
            var items = document.QuerySelectorAll("span.bt-name a");
                //.Where(item => item.ClassName != null && item.ClassName.Contains("block-tov"));

            foreach (var item in items)
            {
                list.Add(item.Attributes["href"].Value);
            }

            return list.ToArray();
        }
    }
}
