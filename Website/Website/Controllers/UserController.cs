using Microsoft.AspNetCore.Mvc;

namespace Website.Controllers
{
    public class UserController : Controller
    {
        public List<UserController> Users;
        public IActionResult Index()
        {
            return View();
        }
    }
}
