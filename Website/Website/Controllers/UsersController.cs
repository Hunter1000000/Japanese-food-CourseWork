using Microsoft.AspNetCore.Mvc;
using Website.DB;
using Website.Models;

namespace Website.Controllers
{
    public class UsersController : Controller
    {
        public AppDBContexts appDb = new AppDBContexts();



        [HttpGet]
        public IActionResult Users()
        {
            var newUser = appDb.usersDB.ToList();
            return View(newUser);
        }

        [HttpPost]
        public IActionResult DeletePurchaseItem(int id)
        {
            // Удалите элемент из списка по его ID
            var item = appDb.usersDB.Find(id);

            if (item == null)
            {
                return NotFound(); // Элемент не найден
            }

            appDb.usersDB.Remove(item);
            appDb.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
