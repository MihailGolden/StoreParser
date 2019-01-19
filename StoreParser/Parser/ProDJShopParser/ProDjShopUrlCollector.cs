using AngleSharp.Dom.Html;
using StoreParser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.ProDjShopUrlCollector
{
    class ProDjShopUrlCollector : IUrlCollector<string[]>
    {
        //IUrlCollector<string[]> _collector;

        //public ProDjShopUrlCollector(IUrlCollector<string[]> collector)
        //{
        //    _collector = collector;
        //}

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
