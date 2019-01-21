using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.Interfaces
{
    public interface IProductParserSettings
    {
        string Url { get; set; }
        string PricePattern { get; set; }
        string PriceAttributeKey { get; set; }
        string HeaderPattern { get; set; }
        string DescriptionPattern { get; set; }
        string ImagePattern { get; set; }
    }
}
