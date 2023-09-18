using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Website.DB;
using System.Web;
using Website.Models;

namespace Website.Controllers
{
    public class ProductsController : Controller
    {
        public AppDBContexts appDb = new AppDBContexts();
        [HttpGet]
        public IActionResult Products()
        {
            var products = appDb.productsDB.ToList();
            return View(products);
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(AddProduct product, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                // Сохраните информацию о продукте, кроме фото, в базе данных.
                // Здесь вы можете использовать Entity Framework для этой цели.

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    string[] substrings = fileName.Split('.');
                    if (substrings.Length > 0 && appDb.productsDB.Any())
                    {
                        fileName = (appDb.productsDB.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1).ToString() + "." + substrings[substrings.Length - 1];
                    }
                    else if(substrings.Length > 0)
                    {
                        fileName = "1." + substrings[substrings.Length - 1];
                    }


                    if (!Directory.Exists("Images/Products"))
                    {
                        Directory.CreateDirectory("Images/Products");
                    }
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/Products/", fileName); // Путь к папке Images в вашем проекте ASP.NET Core
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    // Сохраните путь к изображению в модели продукта.
                    appDb.productsDB.Add(new ProductModel(product.Name, product.Description, product.Company, product.Price, product.Type, "Images/Products/" + fileName));
                    appDb.SaveChanges();

                    return RedirectToAction("Index", "Home");
                } // Перенаправьте пользователя на страницу со списком продуктов или другую подходящую страницу.
            }
            ModelState.AddModelError(string.Empty, "Данные не совпадают");
            return View(product);
        }

        [HttpPost]
        public ActionResult DeletePurchaseItem(int id)
        {
                var item = appDb.productsDB.Find(id);

                if (item == null)
                {
                    return NotFound(); // Элемент не найден
                }

                appDb.productsDB.Remove(item);
                appDb.SaveChanges();

                return RedirectToAction("Index", "Home");
        }
    }
}
