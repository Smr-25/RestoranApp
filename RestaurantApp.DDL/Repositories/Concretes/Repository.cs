using Microsoft.EntityFrameworkCore;
using RestaurantApp.Core.Common;
using RestaurantApp.DDL.Data;
using RestaurantApp.DDL.Repositories.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace RestaurantApp.DDL.Repositories.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly RestaurantDbContext _context;
        public Repository(RestaurantDbContext context)
        {
            _context = context;
        }
        private DbSet<T> Table => _context.Set<T>();
        public IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression = null, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            if (expression != null)
            {
                query = query.Where(expression);
            }
            return query;
        }
        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(expression);
        }
        public async Task<T?> GetByIdAsync(int id, params string[] includes)
        {
            var query = Table.AsQueryable();
            if (includes != null)
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }
        public void Remove(T entity)
        {
            Table.Remove(entity);
        }
        public void Update(T entity)
        {
            Table.Update(entity);
        }
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
