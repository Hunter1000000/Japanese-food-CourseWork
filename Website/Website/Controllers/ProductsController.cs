using Microsoft.AspNetCore.Mvc;
using Website.DB;
using Website.Models;

namespace Website.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Products()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProductsGet()
        {
            return View();
        }
    }
}
