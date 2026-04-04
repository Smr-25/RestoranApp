using RestaurantApp.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace RestaurantApp.DDL.Repositories.Intefaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression = null, params string[] includes);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression, params string[] includes);
        Task<T?> GetByIdAsync(int id, params string[] includes);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<int> CommitAsync();
    }
}
