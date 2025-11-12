using RestaurantApp.BBL.Dtos.MenuItems;
using RestaurantApp.BBL.Dtos.Orders;
using RestaurantApp.BBL.Dtos.OrderItems;

namespace RestaurantApp.PL.Executors
{
    public class OrderExecutor
    {
        private readonly IOrderService _orderService;
        private readonly IMenuItemService _menuItemService;

        public OrderExecutor(IOrderService orderService, IMenuItemService menuItemService)
        {
            _orderService = orderService;
            _menuItemService = menuItemService;
        }

        public async Task ExecuteAddOrderAsync()
        {
            Console.WriteLine("\n=== Yeni Sifariş Əlavə Et ===");

            try
            {
                var itemDtos = await _menuItemService.GetAllMenuItemsAsync();

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

                var orderItems = new List<OrderItemCreateDto>();

                while (true)
                {
                    MenuItemNo:
                    Console.Write("\nMenu item nömrəsi (0 - bitirmək): ");
                    string itemIdInput = Console.ReadLine()!;
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
                    string countInput = Console.ReadLine()!;
                    if (!int.TryParse(countInput, out int count) || count <= 0)
                    {
                        Console.WriteLine("Yanlış say! Yenidən cəhd edin.");
                        goto Count;
                    }

                    var existingItem = orderItems.FirstOrDefault(oi => oi.MenuItemId == itemId);
                    if (existingItem != null)
                    {
                        existingItem.Count += count;
                    }
                    else
                    {
                        orderItems.Add(new OrderItemCreateDto { MenuItemId = itemId, Count = count });
                    }

                    Console.WriteLine($"Əlavə edildi: {count} ədəd");
                }

                var dto = new OrderCreateDto { OrderItems = orderItems };
                await _orderService.AddOrderAsync(dto);
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
            string idInput = Console.ReadLine()!;
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
                var orderDtos = await _orderService.GetAllOrdersAsync();

                if (!orderDtos.Any())
                {
                    Console.WriteLine("Heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine(OrderReturnDto.GetHeader());
                Console.WriteLine(OrderReturnDto.GetSeparator());
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
            string startDateInput = Console.ReadLine()!;
            if (!DateTime.TryParse(startDateInput, out DateTime startDate))
            {
                Console.WriteLine("Yanlış tarix formatı! Yenidən cəhd edin.");
                goto StartDate;
            }

            EndDate:
            Console.Write("Bitmə tarixi (yyyy-MM-dd): ");
            string endDateInput = Console.ReadLine()!;
            if (!DateTime.TryParse(endDateInput, out DateTime endDate))
            {
                Console.WriteLine("Yanlış tarix formatı! Yenidən cəhd edin.");
                goto EndDate;
            }

            try
            {
                var orderDtos = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);

                if (!orderDtos.Any())
                {
                    Console.WriteLine("Bu tarix aralığında heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine(OrderReturnDto.GetHeader());
                Console.WriteLine(OrderReturnDto.GetSeparator());
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
            string minPriceInput = Console.ReadLine()!;
            if (!decimal.TryParse(minPriceInput, out decimal minPrice))
            {
                Console.WriteLine("Yanlış məbləğ formatı! Yenidən cəhd edin.");
                goto MinPrice;
            }

            MaxPrice:
            Console.Write("Maksimum məbləğ: ");
            string maxPriceInput = Console.ReadLine()!;
            if (!decimal.TryParse(maxPriceInput, out decimal maxPrice))
            {
                Console.WriteLine("Yanlış məbləğ formatı! Yenidən cəhd edin.");
                goto MaxPrice;
            }

            try
            {
                var orderDtos = await _orderService.GetOrdersByPriceIntervalAsync(minPrice, maxPrice);

                if (!orderDtos.Any())
                {
                    Console.WriteLine("Bu məbləğ aralığında heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine(OrderReturnDto.GetHeader());
                Console.WriteLine(OrderReturnDto.GetSeparator());
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

            DateInput:
            Console.Write("Tarix (yyyy-MM-dd): ");
            string dateInput = Console.ReadLine()!;
            if (!DateTime.TryParse(dateInput, out DateTime date))
            {
                Console.WriteLine("Yanlış tarix formatı! Yenidən cəhd edin.");
                goto DateInput;
            }

            try
            {
                var orderDtos = await _orderService.GetOrdersByDateAsync(date);

                if (!orderDtos.Any())
                {
                    Console.WriteLine("Bu tarixdə heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine(OrderReturnDto.GetHeader());
                Console.WriteLine(OrderReturnDto.GetSeparator());
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
            Console.WriteLine("\n=== Sifariş Detalları ===");
            
            OrderNo:
            Console.Write("Sifariş nömrəsi: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                goto OrderNo;
            }

            try
            {
                var orderDto = await _orderService.GetOrderByIdAsync(id);
                if (orderDto == null)
                {
                    Console.WriteLine("Sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine(orderDto.ToDetailedString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public void ShowOrderOperationsMenuAsync()
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

