using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Website.DB;
using Website.Models;

namespace Website.Controllers
{
    public class AccountController : Controller
    {
        public AppDBContexts appDb = new AppDBContexts();

        public ActionResult Sign_up()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                foreach (var user_ in appDb.usersDB)
                {
                    if ((user.Email == user_.Email) || (user.Login == user_.Login))
                    {
                        ModelState.AddModelError(string.Empty, "Email или логин уже существуют");
                        return View(user);
                    }
                }
                    appDb.usersDB.Add(new UserModel(user.Name, user.Surname, user.Email, user.Login, user.Password, null, false, false));
                    appDb.SaveChanges();
                    return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                foreach(var user in appDb.usersDB)
                {
                    if((user.Email == loginModel.Email) && (user.Password == loginModel.Password))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Неверный email или пароль.");
            }
            return View(loginModel);
        }
        public ActionResult RegistrationConfirmation()
        {
            return View();
        }
    }
}
