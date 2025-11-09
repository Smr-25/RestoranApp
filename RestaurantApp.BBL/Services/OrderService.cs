namespace RestaurantApp.BBL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<MenuItem> _menuItemRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<MenuItem> menuItemRepository)
        {
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task AddOrderAsync(Dictionary<int, int> menuItemsWithCounts)
        {
            if (menuItemsWithCounts == null || !menuItemsWithCounts.Any())
                throw new ArgumentException("Order must contain at least one item.");

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in menuItemsWithCounts)
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(item.Key);
                if (menuItem == null)
                    throw new InvalidOperationException($"MenuItem with id {item.Key} not found.");
                
                if (item.Value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(item.Value), "Count must be greater than zero.");

                orderItems.Add(new OrderItem
                {
                    MenuItemId = menuItem.Id,
                    Count = item.Value
                });

                totalAmount += menuItem.Price * item.Value;
            }

            var order = new Order
            {
                Date = DateTime.UtcNow,
                TotalAmount = totalAmount,
                OrderItems = orderItems
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task RemoveOrderAsync(int id)
        {
            var entity = await _orderRepository.GetByIdAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"Order with id {id} not found.");

            await _orderRepository.RemoveAsync(entity);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersByDateAsync(DateTime date)
        {
            return await _orderRepository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.Date.Date == date.Date)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<Order>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice)
        {
            return await _orderRepository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _orderRepository.Table
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.Date.Date >= startDate.Date && o.Date.Date <= endDate.Date)
                .ToListAsync();
        }
    }
}
