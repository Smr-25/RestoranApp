using Microsoft.EntityFrameworkCore;
using RestaurantApp.Core.Models;

namespace RestaurantApp.DDL.Data
{

    public class RestaurantDbContext : DbContext
    {
        DbSet<Order> Orders {  get; set; }

        DbSet<OrderItem> OrderItems { get; set; }

        DbSet<MenuItem> MenuItems { get; set; }

        DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MOON01\\SQLEXPRESS;Database=RestaurantAppDb;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
