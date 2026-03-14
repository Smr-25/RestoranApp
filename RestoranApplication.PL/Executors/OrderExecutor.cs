using RestaurantApp.BBL.Dtos.MenuItems;
using RestaurantApp.BBL.Dtos.Orders;
using RestaurantApp.BBL.Dtos.OrderItems;
using RestaurantApp.PL.Helpers;

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
            ConsoleHelper.WriteHeader("\n=== Yeni Sifariş Əlavə Et ===");

            try
            {
                var itemDtos = await _menuItemService.GetAllMenuItemsAsync();

                if (!itemDtos.Any())
                {
                    ConsoleHelper.WriteWarning("Menu boşdur. Əvvəlcə menu item əlavə edin.");
                    return;
                }

                ConsoleHelper.WriteInfo("\nMövcud Menu Item-lar:");
                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }

                var orderItems = new List<OrderItemCreateDto>();

                while (true)
                {
                    MenuItemNo:
                    ConsoleHelper.WritePrompt("\nMenu item nömrəsi (0 - bitirmək): ");
                    string itemIdInput = Console.ReadLine()!;
                    if (!int.TryParse(itemIdInput, out int itemId))
                    {
                        ConsoleHelper.WriteError("Yanlış format! Yenidən cəhd edin.");
                        goto MenuItemNo;
                    }

                    if (itemId == 0)
                    {
                        if (orderItems.Count == 0)
                        {
                            ConsoleHelper.WriteWarning("Sifariş ən azı 1 item olmalıdır!");
                            continue;
                        }

                        break;
                    }

                    Count:
                    ConsoleHelper.WritePrompt("Say: ");
                    string countInput = Console.ReadLine()!;
                    if (!int.TryParse(countInput, out int count) || count <= 0)
                    {
                        ConsoleHelper.WriteError("Yanlış say! Yenidən cəhd edin.");
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

                    ConsoleHelper.WriteSuccess($"Əlavə edildi: {count} ədəd");
                }

                var dto = new OrderCreateDto { OrderItems = orderItems };
                await _orderService.AddOrderAsync(dto);
                ConsoleHelper.WriteSuccess("\nSifariş uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteRemoveOrderAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Sifarişi Ləğv Et ===");

            OrderNo:
            ConsoleHelper.WritePrompt("Silinəcək sifariş nömrəsi: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto OrderNo;
            }

            try
            {
                await _orderService.RemoveOrderAsync(id);
                ConsoleHelper.WriteSuccess("Sifariş uğurla ləğv edildi!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowAllOrdersAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Bütün Sifarişlər ===");

            try
            {
                var orderDtos = await _orderService.GetAllOrdersAsync();

                if (!orderDtos.Any())
                {
                    ConsoleHelper.WriteWarning("Heç bir sifariş tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(OrderReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(OrderReturnDto.GetSeparator());
                foreach (var order in orderDtos)
                {
                    Console.WriteLine(order);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowOrdersByDateRangeAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Tarix Aralığına Görə Sifarişlər ===");

            StartDate:
            ConsoleHelper.WritePrompt("Başlanğıc tarixi (yyyy-MM-dd): ");
            string startDateInput = Console.ReadLine()!;
            if (!DateTime.TryParse(startDateInput, out DateTime startDate))
            {
                ConsoleHelper.WriteError("Yanlış tarix formatı! Yenidən cəhd edin.");
                goto StartDate;
            }

            EndDate:
            ConsoleHelper.WritePrompt("Bitmə tarixi (yyyy-MM-dd): ");
            string endDateInput = Console.ReadLine()!;
            if (!DateTime.TryParse(endDateInput, out DateTime endDate))
            {
                ConsoleHelper.WriteError("Yanlış tarix formatı! Yenidən cəhd edin.");
                goto EndDate;
            }

            try
            {
                var orderDtos = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);

                if (!orderDtos.Any())
                {
                    ConsoleHelper.WriteWarning("Bu tarix aralığında heç bir sifariş tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(OrderReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(OrderReturnDto.GetSeparator());
                foreach (var order in orderDtos)
                {
                    Console.WriteLine(order);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowOrdersByPriceRangeAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Məbləğ Aralığına Görə Sifarişlər ===");

            MinPrice:
            ConsoleHelper.WritePrompt("Minimum məbləğ: ");
            string minPriceInput = Console.ReadLine()!;
            if (!decimal.TryParse(minPriceInput, out decimal minPrice))
            {
                ConsoleHelper.WriteError("Yanlış məbləğ formatı! Yenidən cəhd edin.");
                goto MinPrice;
            }

            MaxPrice:
            ConsoleHelper.WritePrompt("Maksimum məbləğ: ");
            string maxPriceInput = Console.ReadLine()!;
            if (!decimal.TryParse(maxPriceInput, out decimal maxPrice))
            {
                ConsoleHelper.WriteError("Yanlış məbləğ formatı! Yenidən cəhd edin.");
                goto MaxPrice;
            }

            try
            {
                var orderDtos = await _orderService.GetOrdersByPriceIntervalAsync(minPrice, maxPrice);

                if (!orderDtos.Any())
                {
                    ConsoleHelper.WriteWarning("Bu məbləğ aralığında heç bir sifariş tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(OrderReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(OrderReturnDto.GetSeparator());
                foreach (var order in orderDtos)
                {
                    Console.WriteLine(order);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowOrdersByDateAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Tarixə Görə Sifarişlər ===");

            DateInput:
            ConsoleHelper.WritePrompt("Tarix (yyyy-MM-dd): ");
            string dateInput = Console.ReadLine()!;
            if (!DateTime.TryParse(dateInput, out DateTime date))
            {
                ConsoleHelper.WriteError("Yanlış tarix formatı! Yenidən cəhd edin.");
                goto DateInput;
            }

            try
            {
                var orderDtos = await _orderService.GetOrdersByDateAsync(date);

                if (!orderDtos.Any())
                {
                    ConsoleHelper.WriteWarning("Bu tarixdə heç bir sifariş tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(OrderReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(OrderReturnDto.GetSeparator());
                foreach (var order in orderDtos)
                {
                    Console.WriteLine(order);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowOrderByNoAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Sifariş Detalları ===");
            
            OrderNo:
            ConsoleHelper.WritePrompt("Sifariş nömrəsi: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto OrderNo;
            }

            try
            {
                var orderDto = await _orderService.GetOrderByIdAsync(id);
                if (orderDto == null)
                {
                    ConsoleHelper.WriteWarning("Sifariş tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(orderDto.ToDetailedString());
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public void ShowOrderOperationsMenuAsync()
        {
            Console.WriteLine();
            ConsoleHelper.WriteHeader(new string('=', 50));
            ConsoleHelper.WriteHeader("   SİFARİŞ ƏMƏLİYYATLARI");
            ConsoleHelper.WriteHeader(new string('=', 50));
            ConsoleHelper.WriteInfo("1. Yeni sifariş əlavə et");
            ConsoleHelper.WriteInfo("2. Sifarişin ləğvi");
            ConsoleHelper.WriteInfo("3. Bütün sifarişlərin ekrana çıxarılması");
            ConsoleHelper.WriteInfo("4. Tarix aralığına görə sifarişlərin göstərilməsi");
            ConsoleHelper.WriteInfo("5. Məbləğ aralığına görə sifarişlərin göstərilməsi");
            ConsoleHelper.WriteInfo("6. Bir tarixdə olan sifarişlərin göstərilməsi");
            ConsoleHelper.WriteInfo("7. Nömrəyə əsasən sifarişin məlumatlarının göstərilməsi");
            ConsoleHelper.WriteInfo("0. Əvvəlki menyuya qayıt");
            ConsoleHelper.WriteHeader(new string('=', 50));
            ConsoleHelper.WritePrompt("Seçiminiz: ");
        }
    }
}
