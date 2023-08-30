using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }

    }
}
