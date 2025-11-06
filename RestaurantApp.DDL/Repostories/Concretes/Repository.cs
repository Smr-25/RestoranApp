using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RestaurantApp.DDL.Common;
using RestaurantApp.DDL.Data;
using RestaurantApp.DDL.Repostories.Intefaces;

namespace RestaurantApp.DDL.Repostories.Concretes
{
    public class Repository<T> : IRepository<T>where T : BaseEntity
    {
        private readonly RestaurantDbContext _context;

        public Repository(RestaurantDbContext restaurantDbContext)
        {
            _context = restaurantDbContext;
        }

        public DbSet<T> Table {  get; set; }


    }
}
