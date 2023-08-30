using Microsoft.AspNetCore.Mvc;
using Website.DB;
using Website.Models;

namespace Website.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();
        public IActionResult Products()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ProductsGet()
        {
            _context.Products.Add(new ProductModel(1, "Sushi", "Bla bla bla", "Good company", 120.2f, "food", "Images/Products/Hosomaki-Titelbild"));
            var products = _context.Products.ToList(); // Получаем всех пользователей из базы данных
            return View(products);
        }
    }
}
