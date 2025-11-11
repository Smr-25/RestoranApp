using RestaurantApp.BBL.Exceptions;

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
                throw new OrderNotFoundException("Order must contain at least one item.");

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in menuItemsWithCounts)
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(item.Key);
                if (menuItem == null)
                    throw new MenuItemNotFoundException($"MenuItem with id {item.Key} not found.");

                if (item.Value <= 0)
                    throw new CountZeroException("Count must be greater than zero.");

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
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new OrderNotFoundException($"Order with id {id} not found.");

            await _orderRepository.RemoveAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersByDateAsync(DateTime date)
        {
            var orders = await _orderRepository.GetAllAsync(o=>o.Date == date,
                q=>q.Include(o=>o.OrderItems)
                    .ThenInclude(oi=>oi.MenuItem));
            return await orders.ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id,
                q=>q.Include(o=>o.OrderItems)
                    .ThenInclude(oi=>oi.MenuItem));
        }

        public async Task<List<Order>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice)
        {
            var orders = await _orderRepository.GetAllAsync(o=>o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice,
                q=>q.Include(o=>o.OrderItems)
                    .ThenInclude(oi=>oi.MenuItem));
            return await orders.ToListAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return await orders.ToListAsync();
        }

        public async Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetAllAsync(o=>o.Date >= startDate && o.Date <= endDate,
                q=>q.Include(o=>o.OrderItems)
                    .ThenInclude(oi=>oi.MenuItem));
            return await orders.ToListAsync();
        }
    }
}