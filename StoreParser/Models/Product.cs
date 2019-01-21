using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace StoreParser.Models
{
    [Produces("application/json")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public virtual List<Image> Images { get; set; }
        public virtual List<Price> Prices { get; set; }
        public Product()
        {
            Images = new List<Image>();
            Prices = new List<Price>();
        }
    }
}
