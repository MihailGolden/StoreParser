using StoreParser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.ProDjShopUrlCollector
{
    class ProDjShopUrlCollectorSettings : IUrlCollectorSettings
    {
        public string BaseUrl { get; set; } = "https://www.prodj.com.ua/studio-monitors";

        public string Prefix { get; set; } = "?page={CurrentId}";

        public int StartPoint { get; set; } = 1;

        public int EndPoint { get; set; } = 10;
    }
}
