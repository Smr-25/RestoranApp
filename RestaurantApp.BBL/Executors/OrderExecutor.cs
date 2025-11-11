namespace RestaurantApp.BBL.Executors
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

        public async Task ExecuteAsync()
        {
            while (true)
            {
                Console.WriteLine("\n=== SİFARİŞ ƏMƏLİYYATLARI ===");
                Console.WriteLine("1. Yeni sifariş əlavə et");
                Console.WriteLine("2. Sifarişin ləğvi");
                Console.WriteLine("3. Bütün sifarişləri göstər");
                Console.WriteLine("4. Tarix aralığına görə sifarişləri göstər");
                Console.WriteLine("5. Məbləğ aralığına görə sifarişləri göstər");
                Console.WriteLine("6. Verilmiş tarixdə olan sifarişləri göstər");
                Console.WriteLine("7. Nömrəyə əsasən sifarişin məlumatlarını göstər");
                Console.WriteLine("0. Əvvəlki menyuya qayıt");
                Console.Write("\nSeçim edin: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddOrderAsync();
                        break;
                    case "2":
                        await RemoveOrderAsync();
                        break;
                    case "3":
                        await ShowAllOrdersAsync();
                        break;
                    case "4":
                        await ShowOrdersByDateRangeAsync();
                        break;
                    case "5":
                        await ShowOrdersByPriceRangeAsync();
                        break;
                    case "6":
                        await ShowOrdersByDateAsync();
                        break;
                    case "7":
                        await ShowOrderByIdAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
                        break;
                }
            }
        }

        private async Task AddOrderAsync()
        {
            try
            {
                // Menu itemləri göstər
                var menuItems = await _menuItemService.GetAllMenuItemsAsync();
                
                if (menuItems.Count == 0)
                {
                    Console.WriteLine("Heç bir menu item mövcud deyil!");
                    return;
                }

                Console.WriteLine("\n=== MÖVCUD MENU İTEMLƏR ===");
                Console.WriteLine($"{"ID",-5} {"Ad",-20} {"Qiymət",-10}");
                Console.WriteLine(new string('-', 40));
                
                foreach (var item in menuItems)
                {
                    Console.WriteLine($"{item.Id,-5} {item.Name,-20} {item.Price,-10:C}");
                }

                var orderDict = new Dictionary<int, int>();
                bool addingItems = true;

                while (addingItems)
                {
                    Console.Write("\nMenu item ID (0 - bitirmək): ");
                    if (!int.TryParse(Console.ReadLine(), out int menuItemId))
                    {
                        Console.WriteLine("Yanlış ID formatı!");
                        continue;
                    }

                    if (menuItemId == 0)
                    {
                        if (orderDict.Count == 0)
                        {
                            Console.WriteLine("Ən azı bir item əlavə etməlisiniz!");
                            continue;
                        }
                        addingItems = false;
                        break;
                    }

                    Console.Write("Say: ");
                    if (!int.TryParse(Console.ReadLine(), out int count) || count <= 0)
                    {
                        Console.WriteLine("Yanlış say formatı!");
                        continue;
                    }

                    if (orderDict.ContainsKey(menuItemId))
                    {
                        orderDict[menuItemId] += count;
                    }
                    else
                    {
                        orderDict[menuItemId] = count;
                    }

                    Console.WriteLine($"✓ Əlavə edildi! Cari sifariş: {orderDict.Count} növ məhsul, {orderDict.Sum(x => x.Value)} ədəd");
                }

                await _orderService.AddOrderAsync(orderDict);
                Console.WriteLine("\n✓ Sifariş uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task RemoveOrderAsync()
        {
            try
            {
                Console.Write("Silinəcək sifariş ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Yanlış ID formatı!");
                    return;
                }

                await _orderService.RemoveOrderAsync(id);
                Console.WriteLine("Sifariş uğurla ləğv edildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task ShowAllOrdersAsync()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                
                if (orders.Count == 0)
                {
                    Console.WriteLine("Heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine("\n=== BÜTÜN SİFARİŞLƏR ===");
                Console.WriteLine($"{"ID",-5} {"Məbləğ",-15} {"Item Say",-10} {"Tarix",-20}");
                Console.WriteLine(new string('-', 55));
                
                foreach (var order in orders)
                {
                    int totalItemCount = order.OrderItems?.Sum(oi => oi.Count) ?? 0;
                    Console.WriteLine($"{order.Id,-5} {order.TotalAmount,-15:C} {totalItemCount,-10} {order.Date,-20:dd/MM/yyyy HH:mm}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task ShowOrdersByDateRangeAsync()
        {
            try
            {
                Console.Write("Başlanğıc tarixi (dd/MM/yyyy): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                {
                    Console.WriteLine("Yanlış tarix formatı!");
                    return;
                }

                Console.Write("Bitmə tarixi (dd/MM/yyyy): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    Console.WriteLine("Yanlış tarix formatı!");
                    return;
                }

                var orders = await _orderService.GetOrdersByDateRangeAsync(startDate, endDate);
                
                if (orders.Count == 0)
                {
                    Console.WriteLine("Bu tarix aralığında heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine("\n=== SİFARİŞLƏR ===");
                Console.WriteLine($"{"ID",-5} {"Məbləğ",-15} {"Item Say",-10} {"Tarix",-20}");
                Console.WriteLine(new string('-', 55));
                
                foreach (var order in orders)
                {
                    int totalItemCount = order.OrderItems?.Sum(oi => oi.Count) ?? 0;
                    Console.WriteLine($"{order.Id,-5} {order.TotalAmount,-15:C} {totalItemCount,-10} {order.Date,-20:dd/MM/yyyy HH:mm}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task ShowOrdersByPriceRangeAsync()
        {
            try
            {
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

                var orders = await _orderService.GetOrdersByPriceIntervalAsync(minPrice, maxPrice);
                
                if (orders.Count == 0)
                {
                    Console.WriteLine("Bu məbləğ aralığında heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine("\n=== SİFARİŞLƏR ===");
                Console.WriteLine($"{"ID",-5} {"Məbləğ",-15} {"Item Say",-10} {"Tarix",-20}");
                Console.WriteLine(new string('-', 55));
                
                foreach (var order in orders)
                {
                    int totalItemCount = order.OrderItems?.Sum(oi => oi.Count) ?? 0;
                    Console.WriteLine($"{order.Id,-5} {order.TotalAmount,-15:C} {totalItemCount,-10} {order.Date,-20:dd/MM/yyyy HH:mm}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task ShowOrdersByDateAsync()
        {
            try
            {
                Console.Write("Tarix (dd/MM/yyyy): ");
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.WriteLine("Yanlış tarix formatı!");
                    return;
                }

                var orders = await _orderService.GetOrdersByDateAsync(date);
                
                if (orders.Count == 0)
                {
                    Console.WriteLine("Bu tarixdə heç bir sifariş tapılmadı.");
                    return;
                }

                Console.WriteLine("\n=== SİFARİŞLƏR ===");
                Console.WriteLine($"{"ID",-5} {"Məbləğ",-15} {"Item Say",-10} {"Tarix",-20}");
                Console.WriteLine(new string('-', 55));
                
                foreach (var order in orders)
                {
                    int totalItemCount = order.OrderItems?.Sum(oi => oi.Count) ?? 0;
                    Console.WriteLine($"{order.Id,-5} {order.TotalAmount,-15:C} {totalItemCount,-10} {order.Date,-20:dd/MM/yyyy HH:mm}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task ShowOrderByIdAsync()
        {
            try
            {
                Console.Write("Sifariş ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Yanlış ID formatı!");
                    return;
                }

                var order = await _orderService.GetOrderByIdAsync(id);
                
                if (order == null)
                {
                    Console.WriteLine("Sifariş tapılmadı.");
                    return;
                }

                int totalItemCount = order.OrderItems?.Sum(oi => oi.Count) ?? 0;

                Console.WriteLine("\n=== SİFARİŞ MƏLUMATLARI ===");
                Console.WriteLine($"ID: {order.Id}");
                Console.WriteLine($"Məbləğ: {order.TotalAmount:C}");
                Console.WriteLine($"Item Say: {totalItemCount}");
                Console.WriteLine($"Tarix: {order.Date:dd/MM/yyyy HH:mm}");
                
                if (order.OrderItems != null && order.OrderItems.Count > 0)
                {
                    Console.WriteLine("\n=== SİFARİŞ İTEMLƏRİ ===");
                    Console.WriteLine($"{"ID",-5} {"Ad",-20} {"Say",-10}");
                    Console.WriteLine(new string('-', 40));
                    
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"{item.MenuItemId,-5} {item.MenuItem?.Name,-20} {item.Count,-10}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }
    }
}

