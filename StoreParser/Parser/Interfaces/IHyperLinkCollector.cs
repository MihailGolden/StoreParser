using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.Interfaces
{
    class IHyperLinkCollector
    {

        private List<string> Links;
        private string pattern;
        private string BaseHyperLink;

        IHyperLinkCollector(string QuerySelectorPattern) { }

        //void CollectHyperLinks();
    }
}
