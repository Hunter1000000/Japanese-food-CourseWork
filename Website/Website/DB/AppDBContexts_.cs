
using System.Data.Entity;
using Website.Models;

namespace Website.DB
{
    public class AppDBContexts_ : DbContext
    {
        public DbSet<UserModel> usersDB { get; set; }
        public DbSet<ProductModel> productsDB { get; set; }
    }
}
