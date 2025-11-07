using RestaurantApp.BBL.Interfaces;
using RestaurantApp.Core.Models;
using RestaurantApp.DDL.Repostories.Intefaces;
using Microsoft.EntityFrameworkCore;

namespace RestaurantApp.BBL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task AddOrderAsync(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            await _repository.AddAsync(order);
        }

        public async Task RemoveOrderAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"Order with id {id} not found.");

            await _repository.RemoveAsync(entity);
        }

        public async Task<Order?> GetOrderByDateAsync(DateTime date)
        {
            return await _repository.Table
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Date.Date == date.Date);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _repository.Table
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice)
        {
            return await _repository.Table
                .Include(o => o.OrderItems)
                .Where(o => o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice)
                .ToListAsync();
        }
    }
}
