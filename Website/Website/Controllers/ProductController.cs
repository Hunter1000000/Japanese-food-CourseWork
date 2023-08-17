using Microsoft.AspNetCore.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class ProductController : Controller
    {
        public List<ProductModel> Products;
        public IActionResult Index()
        {
            return View();
        }
    }
}
