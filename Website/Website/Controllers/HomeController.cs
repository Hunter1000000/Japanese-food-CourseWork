using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Website.DB;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public AppDBContexts_____ appDb = new AppDBContexts_____();
        public int Product_id;
        [HttpPost]
        public ActionResult GetProductId(int id)
        {
            Product_id = id;
            return RedirectToAction("ProductInfo", "Home");
        }

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchInput)
        {
            List<ProductModel> products = new List<ProductModel>();
            if (searchInput == null)
            {
                var products_ = appDb.productsDB.ToList();
                return View(products_);
            }
            foreach (var item in appDb.productsDB.ToList())
            {
                if (item.Name == searchInput)
                {
                    products.Add(item);
                }
            }
            return View(products);
        }

        [HttpGet]
        public IActionResult Index(string id)
        {
            List<ProductModel> products = new List<ProductModel>();
            if(id == null)
            {
                var products_ = appDb.productsDB.ToList();
                return View(products_);
            }
            foreach (var item in appDb.productsDB.ToList())
            {
                if(item.Type == id)
                {
                    products.Add(item);
                }
            }
            return View(products);
        }

        [HttpGet]
        public ActionResult ProductInfo(int id)
        {
            var products = appDb.productsDB.ToList();
            ProductModel foundItem = products.FirstOrDefault(item => item.Id == id);
            return View(foundItem);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }
}