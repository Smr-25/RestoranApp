using AutoMapper;
using RestaurantApp.BBL.DTOs;
using RestaurantApp.BBL.Interfaces;

namespace RestaurantApp.PL.Executors
{
    public class OrderExecutor
    {
        private readonly IOrderService _orderService;
        private readonly IMenuItemService _menuItemService;
        private readonly IMapper _mapper;

        public OrderExecutor(IOrderService orderService, IMenuItemService menuItemService, IMapper mapper)
        {
            _orderService = orderService;
            _menuItemService = menuItemService;
            _mapper = mapper;
        }

        public async Task AddOrderAsync()
        {
            Console.WriteLine("\n=== Yeni Sifariş Əlavə Et ===");

            try
            {
                var allItems = await _menuItemService.GetAllMenuItemsAsync();
                var itemDtos = _mapper.Map<List<MenuItemDto>>(allItems);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Menu boşdur. Əvvəlcə menu item əlavə edin.");
                    return;
                }

                Console.WriteLine("\nMövcud Menu Item-lar:");
                Console.WriteLine($"{"Nömrə",-10}{"Ad",-30}{"Qiymət",-10}");
                Console.WriteLine(new string('-', 50));
                foreach (var item in itemDtos)
                {
                    Console.WriteLine($"{item.Id,-10}{item.Name,-30}{item.Price,-10:C}");
                }

                var orderItems = new Dictionary<int, int>();
                
                while (true)
                {
                    Console.Write("\nMenu item nömrəsi (0 - bitirmək): ");
                    if (!int.TryParse(Console.ReadLine(), out int itemId))
                    {
                        Console.WriteLine("Yanlış format!");
                        continue;
                    }

                    if (itemId == 0)
                    {
                        if (orderItems.Count == 0)
                        {
                            Console.WriteLine("Sifariş ən azı 1 item olmalıdır!");
                            continue;
                        }
                        break;
                    }

                    Console.Write("Say: ");
                    if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
                    {
                        Console.WriteLine("Yanlış say!");
                        continue;
                    }

                    if (orderItems.ContainsKey(itemId))
                    {
                        orderItems[itemId] += count;
                    }
                    else
                    {
                        orderItems[itemId] = count;
                    }

                    Console.WriteLine($"Əlavə edildi: {count} ədəd");
                }

                await _orderService.AddOrderAsync(orderItems);
                Console.WriteLine("\nSifariş uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task RemoveOrderAsync()
        {
            Console.WriteLine("\n=== Sifarişi Ləğv Et ===");
            
            Console.Write("Silinəcək sifariş nömrəsi: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Yanlış ID formatı!");
                return;
            }

            try
            {
                await _orderService.RemoveOrderAsync(id);
                Console.WriteLine("Sifariş uğurla ləğv edildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ShowAllOrdersAsync()
        {
            Console.WriteLine("\n=== Bütün Sifarişlər ===");
            
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                if (!orderDtos.Any())
                {
                    Console.WriteLine("Heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine($"{"Nömrə",-10}{"Məbləğ",-15}{"Item Sayı",-15}{"Tarix",-20}");
                Console.WriteLine(new string('-', 60));
                foreach (var order in orderDtos)
                {
                    Console.WriteLine($"{order.Id,-10}{order.TotalAmount,-15:C}{order.TotalItemCount,-15}{order.Date,-20:yyyy-MM-dd HH:mm}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ShowOrdersByDateRangeAsync()
        {
            Console.WriteLine("\n=== Tarix Aralığına Görə Sifarişlər ===");
            
            Console.Write("Başlanğıc tarixi (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Yanlış tarix formatı!");
                return;
            }

            Console.Write("Bitmə tarixi (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Yanlış tarix formatı!");
                return;
            }

            try
            {
                var orders = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);
                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                if (!orderDtos.Any())
                {
                    Console.WriteLine("Bu tarix aralığında heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine($"\n{"Nömrə",-10}{"Məbləğ",-15}{"Item Sayı",-15}{"Tarix",-20}");
                Console.WriteLine(new string('-', 60));
                foreach (var order in orderDtos)
                {
                    Console.WriteLine($"{order.Id,-10}{order.TotalAmount,-15:C}{order.TotalItemCount,-15}{order.Date,-20:yyyy-MM-dd HH:mm}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ShowOrdersByPriceRangeAsync()
        {
            Console.WriteLine("\n=== Məbləğ Aralığına Görə Sifarişlər ===");
            
            Console.Write("Minimum məbləğ: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
            {
                Console.WriteLine("Yanlış məbləğ formatı!");
                return;
            }

            Console.Write("Maksimum məbləğ: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
            {
                Console.WriteLine("Yanlış məbləğ formatı!");
                return;
            }

            try
            {
                var orders = await _orderService.GetOrdersByPriceIntervalAsync(minPrice, maxPrice);
                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                if (!orderDtos.Any())
                {
                    Console.WriteLine("Bu məbləğ aralığında heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine($"\n{"Nömrə",-10}{"Məbləğ",-15}{"Item Sayı",-15}{"Tarix",-20}");
                Console.WriteLine(new string('-', 60));
                foreach (var order in orderDtos)
                {
                    Console.WriteLine($"{order.Id,-10}{order.TotalAmount,-15:C}{order.TotalItemCount,-15}{order.Date,-20:yyyy-MM-dd HH:mm}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ShowOrdersByDateAsync()
        {
            Console.WriteLine("\n=== Tarixə Görə Sifarişlər ===");
            
            Console.Write("Tarix (yyyy-MM-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Yanlış tarix formatı!");
                return;
            }

            try
            {
                var orders = await _orderService.GetOrdersByDateAsync(date);
                var orderDtos = _mapper.Map<List<OrderDto>>(orders);

                if (!orderDtos.Any())
                {
                    Console.WriteLine("Bu tarixdə heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine($"\n{"Nömrə",-10}{"Məbləğ",-15}{"Item Sayı",-15}{"Tarix",-20}");
                Console.WriteLine(new string('-', 60));
                foreach (var order in orderDtos)
                {
                    Console.WriteLine($"{order.Id,-10}{order.TotalAmount,-15:C}{order.TotalItemCount,-15}{order.Date,-20:yyyy-MM-dd HH:mm}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ShowOrderDetailAsync()
        {
            Console.WriteLine("\n=== Sifariş Detalları ===");
            
            Console.Write("Sifariş nömrəsi: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Yanlış ID formatı!");
                return;
            }

            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    Console.WriteLine("Sifariş tapılmadı.");
                    return;
                }

                var orderDto = _mapper.Map<OrderDetailDto>(order);

                Console.WriteLine($"\nNömrə: {orderDto.Id}");
                Console.WriteLine($"Məbləğ: {orderDto.TotalAmount:C}");
                Console.WriteLine($"Item Sayı: {orderDto.TotalItemCount}");
                Console.WriteLine($"Tarix: {orderDto.Date:yyyy-MM-dd HH:mm}");
                
                Console.WriteLine("\nSifariş Item-ları:");
                Console.WriteLine($"{"Nömrə",-10}{"Ad",-30}{"Say",-10}");
                Console.WriteLine(new string('-', 50));
                foreach (var item in orderDto.OrderItems)
                {
                    Console.WriteLine($"{item.Id,-10}{item.MenuItemName,-30}{item.Count,-10}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }
    }
}

