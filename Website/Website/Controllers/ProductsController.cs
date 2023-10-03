using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Website.DB;
using System.Web;
using Website.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Website.Controllers
{
    public class ProductsController : Controller
    {
        public AppDBContexts_ appDb = new AppDBContexts_();
        public int Product_id = 0;
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
                    appDb.productsDB.Add(new ProductModel(product.Name, product.Description, product.Company, product.Price, product.Type, ""));
                    appDb.SaveChanges();
                    var lastelement = appDb.productsDB.OrderByDescending(x => x.Id).FirstOrDefault();

                    var fileName = Path.GetFileName(file.FileName);
                    string[] substrings = fileName.Split('.');
                    if (substrings.Length > 0)
                    {
                        fileName = (lastelement.Id).ToString() + "." + substrings[substrings.Length - 1];
                    }
                    else
                    {
                        fileName = (lastelement.Id).ToString();
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
                    lastelement.PhotoPath = "Images/Products/" + fileName;
                    appDb.SaveChanges();

                    return RedirectToAction("Index", "Home");
                } // Перенаправьте пользователя на страницу со списком продуктов или другую подходящую страницу.
            }
            ModelState.AddModelError(string.Empty, "Данные не совпадают");
            return View(product);
        }

        [HttpGet]
        public ActionResult ProductInfo()
        {
            var products = appDb.productsDB.ToList();
            ProductModel foundItem;
                foundItem = products.FirstOrDefault(item => item.Id == Product_id);
            return View(foundItem);
        }

        [HttpPost]
        public ActionResult DeletePurchaseItem(int id)
        {
            var item = appDb.productsDB.Find(id);
            //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/Products/", fileName)
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.PhotoPath)))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.PhotoPath));
            }

            appDb.productsDB.Remove(item);
            appDb.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
