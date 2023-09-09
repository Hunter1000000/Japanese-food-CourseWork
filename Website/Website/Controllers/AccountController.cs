using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Website.DB;
using Website.Models;

namespace Website.Controllers
{
    public class AccountController : Controller
    {
        public AppDbContent appDb = new AppDbContent();

        [HttpGet]
        public ActionResult Sign_up()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                appDb.Users.Add(user);
                appDb.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }
        public ActionResult RegistrationConfirmation()
        {
            return View();
        }
    }
}
