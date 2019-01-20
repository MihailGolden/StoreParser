using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreParser.Models;

namespace StoreParser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProDjShopController : ControllerBase
    {
        StoreContext db;

        public ProDjShopController(StoreContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            //var prods = db.Products;

            //var products = from entity in prods
            //               select new
            //               {
            //                   name = entity.Name,
            //                   id = entity.Id,
            //                   prices =
            //                             from p in entity.Prices
            //                             select new
            //                             {
            //                                 price = p.ProductPrice,
            //                                 date = p.PriceLastDate
            //                             }
            //               }.ToString();

            var products = db.Products;
            List<Product> productList = new List<Product>();
            foreach (var p in products)
            {
                productList.Add(p);
            }
            return products;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}