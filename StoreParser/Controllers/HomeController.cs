using Microsoft.AspNetCore.Mvc;
using StoreParser.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StoreParser.Controllers
{
    public class HomeController : Controller
    {
        StoreContext db;

        public HomeController(StoreContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }
        
        public async Task<ActionResult> GetProduct(int id)
        {
            Product product = db.Products.Where(p => p.Id == id).FirstOrDefault();
            return View(product);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
