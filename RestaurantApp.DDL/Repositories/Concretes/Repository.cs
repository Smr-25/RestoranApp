using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

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
        
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate )
        {
            return await Table.AnyAsync(predicate);
        }
        
        public async Task<IQueryable<T>> GetAllAsync()
        {
            return Table.AsQueryable();
        }
        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = Table;

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return query;
        }
        
        public async Task<T?> GetByIdAsync(int id,Func<IQueryable<T> , IIncludableQueryable<T, object>>? include = null)
        {
            IQueryable<T> query = Table;

            if (include != null)
            {
                query = include(query);
            }
            
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }
        

    }
}

