using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string HyperLink { get; set; }

        public virtual List<Image> Images { get; set; }
        public virtual List<Price> Prices { get; set; }

        public Product()
        {
            Images = new List<Image>();
            Prices = new List<Price>();
        }
    }
}
