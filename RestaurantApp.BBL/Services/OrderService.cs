namespace RestaurantApp.BBL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;

        public OrderService(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task AddOrderAsync(MenuItem menuItem, int count)
        {
            if (menuItem == null)
                throw new ArgumentNullException(nameof(menuItem));
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero.");

            var orderItem = new OrderItem()
            {
                MenuItemId = menuItem.Id,
                Count = count
            };

            var order = new Order
            {
                Date = DateTime.UtcNow,
                TotalAmount = menuItem.Price * count,
                OrderItems = new List<OrderItem> { orderItem }
            };

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();
        }

        public async Task RemoveOrderAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"Order with id {id} not found.");

            await _repository.RemoveAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<Order?> GetOrderByDateAsync(DateTime date)
        {
            return await _repository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Date.Date == date.Date);
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _repository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice)
        {
            return await _repository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _repository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _repository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.Date.Date >= startDate.Date && o.Date.Date <= endDate.Date)
                .ToListAsync();
        }
    }
}
