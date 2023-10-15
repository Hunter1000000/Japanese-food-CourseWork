using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Website.DB;
using Website.Models;

namespace Website.Controll1ers
{
    public class ProductsController : Controller
    {

        public AppDBContexts_____ appDb = new AppDBContexts_____();
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
        public ActionResult ProductInfo(int id)
        {
            var products = appDb.productsDB.ToList();
            ProductModel foundItem;
                foundItem = products.FirstOrDefault(item => item.Id == id);
            return View(foundItem);
        }

        [HttpPost]
        public ActionResult DeletePurchaseItem(int id)
        {
            var item = appDb.productsDB.FirstOrDefault(item => item.Id == id);
            //Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images/Products/", fileName)
            if (System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.PhotoPath)))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", item.PhotoPath));
            }

            appDb.productsDB.Remove(item);
            appDb.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ChangeProductViews(int id)
        {
            //var item = appDb.productsDB.FirstOrDefault(item => item.Id == id);

            return RedirectToAction("ChangeProduct", "Products", new { id = id });
        }

        [HttpGet]
        public ActionResult Basket()
        {
            List<ProductModel> basket = HttpContext.Session.Get<List<ProductModel>>("Basket");

            // Requires SessionExtensions from sample.
            //if (HttpContext.Session.Get<List<ProductModel>>("Basket") == default)
            //{
               // HttpContext.Session.Set<List<ProductModel>>("Basket", basket);
            //}
            // Отобразите корзину в представлении
            return View(basket);
        }

        [HttpPost]
        public ActionResult AddToBasket(int id)
        {
            List<ProductModel> basket;

            if (HttpContext.Session.Get<List<ProductModel>>("Basket") == null)
            {
                basket = new List<ProductModel>();
                var product = appDb.productsDB.FirstOrDefault(item => item.Id == id);
                basket.Add(product);
                HttpContext.Session.Set<List<ProductModel>>("Basket", basket);
            }
            else
            {
                basket = HttpContext.Session.Get<List<ProductModel>>("Basket");
                var product = appDb.productsDB.FirstOrDefault(item => item.Id == id);
                basket.Add(product);
                HttpContext.Session.Set<List<ProductModel>>("Basket", basket);
            }

            return RedirectToAction("ProductInfo", "Products", new { id = id });
        }

        [HttpPost]
        public ActionResult RemoveFromBasket(int productId)
        {
            List<ProductModel> basket = HttpContext.Session.Get<List<ProductModel>>("Basket");

            if (basket != null)
            {
                // Найдите и удалите товар из корзины
                basket.RemoveAll(item => item.Id == productId);

                // Сохраните изменения в сессии
                HttpContext.Session.Set<List<ProductModel>>("Basket", basket);
            }

            return RedirectToAction("Basket", "Products");
        }

        [HttpGet]
        public ActionResult ChangeProduct(int id)
        {
            var item = appDb.productsDB.FirstOrDefault(item => item.Id == id);

            return View(item);
        }

        [HttpGet]
        public ActionResult Buy(int id)
        {
            var item = appDb.productsDB.FirstOrDefault(item => item.Id == id);

            return View(item);
        }

        [HttpGet]
        public IActionResult CompanyProducts()
        {
            var products = appDb.productsDB.ToList();
            return View(products);
        }

        [HttpPost]
        public IActionResult BuyProduct(BuyProductModelVisa buyProductModelVisa)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Success", "Products");
            }
            return View();
        }

        public IActionResult BuyProduct()
        {
            return View();
        }
        public IActionResult AddCompanyProduct()
        {
            return View();
        }

        public IActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCompanyProduct(ProductModel productModel)
        {
            var item = appDb.usersDB.FirstOrDefault(item => item.Login == User.Identity.Name);
            productModel.Company = item.Company;
            return View();
        }
    }
}
