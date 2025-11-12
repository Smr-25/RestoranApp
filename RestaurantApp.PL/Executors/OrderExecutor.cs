using AutoMapper;
using RestaurantApp.BBL.DTOs.MenuItems;
using RestaurantApp.BBL.DTOs.Orders;
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

        public async Task ExecuteAddOrderAsync()
        {
            Console.WriteLine("\n=== Yeni Sifariş Əlavə Et ===");

            try
            {
                var allItems = await _menuItemService.GetAllMenuItemsAsync();
                var itemDtos = _mapper.Map<List<MenuItemReturnDto>>(allItems);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Menu boşdur. Əvvəlcə menu item əlavə edin.");
                    return;
                }

                Console.WriteLine("\nMövcud Menu Item-lar:");
                Console.WriteLine(MenuItemReturnDto.GetHeader());
                Console.WriteLine(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }

                var orderItems = new Dictionary<int, int>();

                while (true)
                {
                    MenuItemNo:
                    Console.Write("\nMenu item nömrəsi (0 - bitirmək): ");
                    string itemIdInput = Console.ReadLine();
                    if (!int.TryParse(itemIdInput, out int itemId))
                    {
                        Console.WriteLine("Yanlış format! Yenidən cəhd edin.");
                        goto MenuItemNo;
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

                    Count:
                    Console.Write("Say: ");
                    string countInput = Console.ReadLine();
                    if (!int.TryParse(countInput, out int count) || count <= 0)
                    {
                        Console.WriteLine("Yanlış say! Yenidən cəhd edin.");
                        goto Count;
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

        public async Task ExecuteRemoveOrderAsync()
        {
            Console.WriteLine("\n=== Sifarişi Ləğv Et ===");

            OrderNo:
            Console.Write("Silinəcək sifariş nömrəsi: ");
            string idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                goto OrderNo;
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

        public async Task ExecuteShowAllOrdersAsync()
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

                Console.WriteLine(OrderDto.GetHeader());
                Console.WriteLine(OrderDto.GetSeparator());
                foreach (var order in orderDtos)
                {
                    Console.WriteLine(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowOrdersByDateRangeAsync()
        {
            Console.WriteLine("\n=== Tarix Aralığına Görə Sifarişlər ===");

            StartDate:
            Console.Write("Başlanğıc tarixi (yyyy-MM-dd): ");
            string startDateInput = Console.ReadLine();
            if (!DateTime.TryParse(startDateInput, out DateTime startDate))
            {
                Console.WriteLine("Yanlış tarix formatı! Yenidən cəhd edin.");
                goto StartDate;
            }

            EndDate:
            Console.Write("Bitmə tarixi (yyyy-MM-dd): ");
            string endDateInput = Console.ReadLine();
            if (!DateTime.TryParse(endDateInput, out DateTime endDate))
            {
                Console.WriteLine("Yanlış tarix formatı! Yenidən cəhd edin.");
                goto EndDate;
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

                Console.WriteLine(OrderDto.GetHeader());
                Console.WriteLine(OrderDto.GetSeparator());
                foreach (var order in orderDtos)
                {
                    Console.WriteLine(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowOrdersByPriceRangeAsync()
        {
            Console.WriteLine("\n=== Məbləğ Aralığına Görə Sifarişlər ===");

            MinPrice:
            Console.Write("Minimum məbləğ: ");
            string minPriceInput = Console.ReadLine();
            if (!decimal.TryParse(minPriceInput, out decimal minPrice))
            {
                Console.WriteLine("Yanlış məbləğ formatı! Yenidən cəhd edin.");
                goto MinPrice;
            }

            MaxPrice:
            Console.Write("Maksimum məbləğ: ");
            string maxPriceInput = Console.ReadLine();
            if (!decimal.TryParse(maxPriceInput, out decimal maxPrice))
            {
                Console.WriteLine("Yanlış məbləğ formatı! Yenidən cəhd edin.");
                goto MaxPrice;
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

                Console.WriteLine(OrderDto.GetHeader());
                Console.WriteLine(OrderDto.GetSeparator());
                foreach (var order in orderDtos)
                {
                    Console.WriteLine(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowOrdersByDateAsync()
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

                Console.WriteLine(OrderDto.GetHeader());
                Console.WriteLine(OrderDto.GetSeparator());
                foreach (var order in orderDtos)
                {
                    Console.WriteLine(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowOrderByNoAsync()
        {
            
            OrderNo:
            Console.Write("Sifariş nömrəsi: ");
            string idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                goto OrderNo;
            }
            
            Console.WriteLine("\n=== Sifariş Detalları ===");

            try
            {
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    Console.WriteLine("Sifariş tapılmadı.");
                    return;
                }

                var orderDto = _mapper.Map<OrderDto>(order);

                Console.WriteLine(orderDto.ToDetailedString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }
        public async Task ShowOrderOperationsMenuAsync()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("   SİFARİŞ ƏMƏLİYYATLARI");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("1. Yeni sifariş əlavə et");
            Console.WriteLine("2. Sifarişin ləğvi");
            Console.WriteLine("3. Bütün sifarişlərin ekrana çıxarılması");
            Console.WriteLine("4. Tarix aralığına görə sifarişlərin göstərilməsi");
            Console.WriteLine("5. Məbləğ aralığına görə sifarişlərin göstərilməsi");
            Console.WriteLine("6. Bir tarixdə olan sifarişlərin göstərilməsi");
            Console.WriteLine("7. Nömrəyə əsasən sifarişin məlumatlarının göstərilməsi");
            Console.WriteLine("0. Əvvəlki menyuya qayıt");
            Console.WriteLine(new string('=', 50));
            Console.Write("Seçiminiz: ");

        }
    }
}

