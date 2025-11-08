
using Microsoft.EntityFrameworkCore;
using RestaurantApp.DDL.Common;

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
    }
}

