using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace RestaurantApp.DDL.Repostories.Intefaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task RemoveAsync(T entity);

        Task SaveChangesAsync();

        Task<T?> GetByIdAsync(int id);
        
        Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate );
        
        Task<IQueryable<T>> GetAllAsync();
        
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        
        Task<T?> GetByIdAsync(int id,Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    }
}

