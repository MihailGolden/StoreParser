using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Models
{
    public class Price
    {

        public int Id { get; set; }

        public decimal ProductPrice { get; set; }

        public DateTime PriceLastDate { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        public Price() { }
        public Price(decimal price)
        {
            ProductPrice = price;
            PriceLastDate = DateTime.Now;
        }
    }
}
