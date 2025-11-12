namespace RestaurantApp.BBL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;

        public OrderService(IRepository<Order> orderRepository, IRepository<MenuItem> menuItemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task AddOrderAsync(OrderCreateDto dto)
        {
            if (dto.OrderItems == null || !dto.OrderItems.Any())
                throw new CountZeroException("Sifariş ən azı 1 məhsul ehtiva etməlidir.");

            var orderItems = new List<OrderItem>();
            decimal totalAmount = 0;

            foreach (var item in dto.OrderItems)
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
                if (menuItem == null)
                    throw new EntityNotFoundException($"ID-si {item.MenuItemId} olan məhsul tapılmadı.");

                if (item.Count <= 0)
                    throw new CountZeroException("Say sıfırdan böyük olmalıdır.");

                orderItems.Add(new OrderItem
                {
                    MenuItemId = menuItem.Id,
                    Count = item.Count
                });

                totalAmount += menuItem.Price * item.Count;
               
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
                throw new EntityNotFoundException($"ID-si {id} olan sifariş tapılmadı.");

            await _orderRepository.RemoveAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task<List<OrderReturnDto>> GetOrdersByDateAsync(DateTime date)
        {
            var orders = await _orderRepository.GetAllAsync(o=>o.Date.Date == date.Date,
                q=>q.Include(o=>o.OrderItems)
                    .ThenInclude(oi=>oi.MenuItem));
            var orderList = await orders.ToListAsync();
            return _mapper.Map<List<OrderReturnDto>>(orderList);
        }

        public async Task<OrderReturnDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id,
                q=>q.Include(o=>o.OrderItems)
                    .ThenInclude(oi=>oi.MenuItem));
            return order == null ? null : _mapper.Map<OrderReturnDto>(order);
        }

        public async Task<List<OrderReturnDto>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice)
        {
            var orders = await _orderRepository.GetAllAsync(o=>o.TotalAmount >= minPrice && o.TotalAmount <= maxPrice,
                q=>q.Include(o=>o.OrderItems)
                    .ThenInclude(oi=>oi.MenuItem));
            var orderList = await orders.ToListAsync();
            return _mapper.Map<List<OrderReturnDto>>(orderList);
        }

        public async Task<List<OrderReturnDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync(
                 q => q.Include(o => o.OrderItems).ThenInclude(oi => oi.MenuItem));
            var orderList = await orders.ToListAsync();
            return _mapper.Map<List<OrderReturnDto>>(orderList);
        }

        public async Task<List<OrderReturnDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var orders = await _orderRepository.GetAllAsync(o=>o.Date.Date >= startDate && o.Date.Date <= endDate,
                q=>q.Include(o=>o.OrderItems)
                    .ThenInclude(oi=>oi.MenuItem));
            var orderList = await orders.ToListAsync();
            return _mapper.Map<List<OrderReturnDto>>(orderList);
        }
    }
}