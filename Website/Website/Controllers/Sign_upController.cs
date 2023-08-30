using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Website.DB;
using Website.Models;

namespace Website.Controllers
{
    public class Sign_upController : Controller
    {
        private readonly AppDbContext _context = new AppDbContext();
        public ActionResult Sign_up()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sign_up(UserModel model)
        { 
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Пароли не совпадают.");
                    return View(model);
                }
                _context.Users.Add(model);
                _context.SaveChanges();
                return RedirectToAction("RegistrationConfirmation");
            }

            return View(model);
        }

        public ActionResult RegistrationConfirmation()
        {
            return View();
        }
    }
}
