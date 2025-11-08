
namespace RestaurantApp.DDL.Repostories.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly RestaurantDbContext _context;

        public Repository(RestaurantDbContext restaurantDbContext)
        {
            _context = restaurantDbContext;
            Table = _context.Set<T>();
        }

        public DbSet<T> Table { get; set; }

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
             Table.Update(entity);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(T entity)
        {
            Table.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
           await  _context.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }


    }
}

