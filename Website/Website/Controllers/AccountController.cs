using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using Website.DB;
using Website.Interfaces;
using Website.Models;

namespace Website.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _carService;
        public AppDBContexts_ appDb = new AppDBContexts_();

        public ActionResult Sign_up()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sign_up(RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                foreach (var user__ in appDb.usersDB)
                {
                    if ((user.Email == user__.Email) || (user.Login == user__.Login))
                    {
                        ModelState.AddModelError(string.Empty, "Email или логин уже существуют");
                        return View(user);
                    }
                }
                var user_ = new UserModel(user.Name, user.Surname, user.Email, user.Login, user.Password, "", "User");
                var response = new BaseResponse<ClaimsIdentity>
                {
                    Data = Authenticate(user_),
                    Description = "",
                    StatusCode = StatusCodes.Status200OK
                };
                if (response.StatusCode == StatusCodes.Status200OK)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                }
                appDb.usersDB.Add(user_);
                appDb.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {

            if (ModelState.IsValid)
            {
                foreach (var user in appDb.usersDB)
                {
                    if ((user.Email == loginModel.Email) && (user.Password == loginModel.Password))
                    {
                        var response = new BaseResponse<ClaimsIdentity>
                        {
                            Data = Authenticate(user),
                            Description = "",
                            StatusCode = StatusCodes.Status200OK
                        };
                        if (response.StatusCode == StatusCodes.Status200OK)
                        {
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                        }
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

        public ClaimsIdentity Authenticate(UserModel userModel)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userModel.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, userModel.Role)
            };
            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        [HttpGet]
        public IActionResult Users()
        {
            return View(appDb.usersDB.ToList());
        }

        [HttpPost]
        public IActionResult RoleType(string selectDown) 
        {
            string[] parts = selectDown.Split(' ');
            if (!string.IsNullOrEmpty(parts[1]))
            {
                int id = Convert.ToInt32(parts[0]);
                var foundItem = appDb.usersDB.FirstOrDefault(item => item.Id == id);
                foundItem.Role = parts[1];
                appDb.SaveChanges();
            }
            return RedirectToAction("Users", "Account");
        }

        [HttpPost]
        public IActionResult DeletePurchaseItem(int id)
        {
            // Удалите элемент из списка по его ID
            var foundItem = appDb.usersDB.FirstOrDefault(item => item.Id == id);
            //var item = appDb.usersDB.Find(id); <- было

            if (foundItem == null)
            {
                return NotFound(); // Элемент не найден
            }

            appDb.usersDB.Remove(foundItem);
            appDb.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile()
        {
            return View();
        }
    }
}
