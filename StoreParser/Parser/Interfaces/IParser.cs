﻿using AngleSharp.Dom.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.Interfaces
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
