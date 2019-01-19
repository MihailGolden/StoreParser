using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.Interfaces
{
    class IHyperLinkCollector_
    {

        private List<string> Links;
        private string pattern;
        private string BaseHyperLink;

        IHyperLinkCollector_(string QuerySelectorPattern) { }

        //void CollectHyperLinks();
    }
}
