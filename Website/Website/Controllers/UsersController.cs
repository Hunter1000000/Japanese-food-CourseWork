using Microsoft.AspNetCore.Mvc;
using Website.DB;
using Website.Models;

namespace Website.Controllers
{
    public class UsersController : Controller
    {
        public AppDBContexts_ appDb = new AppDBContexts_();
    }
}
