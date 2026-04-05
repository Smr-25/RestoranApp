using System.Linq.Expressions;
using RestaurantApp.Core.Common;
namespace RestaurantApp.DDL.Repositories.Interfaces;
public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression = null, params string[] includes);
    Task<T?> GetAsync(Expression<Func<T, bool>> expression, params string[] includes);
    Task<T?> GetByIdAsync(int id, params string[] includes);
    Task AddAsync(T entity);
    void Remove(T entity);
    void Update(T entity);
    Task<int> CommitAsync();
}
