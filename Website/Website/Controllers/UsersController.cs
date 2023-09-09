using Microsoft.AspNetCore.Mvc;
using Website.DB;
using Website.Models;

namespace Website.Controllers
{
    public class UsersController : Controller
    {
        public AppDbContent appDb = new AppDbContent();



        [HttpGet]
        public IActionResult Users()
        {
            var newUser = appDb.Users.ToList();
            return View(newUser);
        }
    }
}
