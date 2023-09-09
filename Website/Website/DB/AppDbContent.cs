
using System.Data.Entity;
using Website.Models;

namespace Website.DB
{
    public class AppDbContent : DbContext
    {
        public DbSet<RegisterModel> Users { get; set; }
        public DbSet<ProductModel> Products { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisterModel>().HasKey(r => r.Id);
            // Другие настройки модели
        }
    }
}
