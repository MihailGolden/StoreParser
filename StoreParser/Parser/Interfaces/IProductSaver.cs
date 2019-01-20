using Microsoft.EntityFrameworkCore;
using StoreParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Parser.Interfaces
{
    interface IProductSaver<T> where T : class
    {
        Task SaveProducts(List<T> products);
    }
}
