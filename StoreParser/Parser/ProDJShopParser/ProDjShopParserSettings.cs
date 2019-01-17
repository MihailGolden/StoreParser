using StoreParser.Parser.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.ProDJShopParser
{
    class ProDjShopParserSettings : IParserSettings
    {
        public ProDjShopParserSettings(int start, int end)
        {
            StartPoint = start;
            EndPoint = end;
        }

        public string BaseUrl { get; set; } = "https://www.prodj.com.ua/studio-monitors";

        public string Prefix { get; set; } = "?page={CurrentId}";

        public int StartPoint { get; set; }

        public int EndPoint { get; set; }
    }
}
