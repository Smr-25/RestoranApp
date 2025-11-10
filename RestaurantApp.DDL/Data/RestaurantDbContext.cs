using Microsoft.EntityFrameworkCore;
using RestaurantApp.Core.Models;
using RestaurantApp.DDL.Data.Configurations;

namespace RestaurantApp.DDL.Data
{

    public class RestaurantDbContext : DbContext
    {
        public DbSet<Order> Orders {  get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<Category> Categories { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=RestaurantDb;User Id=sa;Password=MyMSSQLPassword@123.;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantDbContext).Assembly);
        
        }
    }
}
