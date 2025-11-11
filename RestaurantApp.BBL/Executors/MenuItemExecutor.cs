namespace RestaurantApp.BBL.Executors
{
    public class MenuItemExecutor
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ICategoryService _categoryService;

        public MenuItemExecutor(IMenuItemService menuItemService, ICategoryService categoryService)
        {
            _menuItemService = menuItemService;
            _categoryService = categoryService;
        }

        public async Task ExecuteAsync()
        {
            while (true)
            {
                Console.WriteLine("\n=== MENU İTEM ƏMƏLİYYATLARI ===");
                Console.WriteLine("1. Yeni item əlavə et");
                Console.WriteLine("2. Item üzərində düzəliş et");
                Console.WriteLine("3. Item sil");
                Console.WriteLine("4. Bütün Item-ları göstər");
                Console.WriteLine("5. Kateqoriyasına görə menu item-ları göstər");
                Console.WriteLine("6. Qiymət aralığına görə menu item-lar göstər");
                Console.WriteLine("7. Menu item-lar arasında ada görə axtarış et");
                Console.WriteLine("0. Əvvəlki menyuya qayıt");
                Console.Write("\nSeçim edin: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await AddMenuItemAsync();
                        break;
                    case "2":
                        await EditMenuItemAsync();
                        break;
                    case "3":
                        await RemoveMenuItemAsync();
                        break;
                    case "4":
                        await ShowAllMenuItemsAsync();
                        break;
                    case "5":
                        await ShowMenuItemsByCategoryAsync();
                        break;
                    case "6":
                        await ShowMenuItemsByPriceRangeAsync();
                        break;
                    case "7":
                        await SearchMenuItemsAsync();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Yanlış seçim! Yenidən cəhd edin.");
                        break;
                }
            }
        }

        private async Task AddMenuItemAsync()
        {
            try
            {
                Console.Write("Menu item adı: ");
                var name = Console.ReadLine();

                Console.Write("Qiymət: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Yanlış qiymət formatı!");
                    return;
                }

                // Kateqoriyaları göstər
                var categories = await _categoryService.GetAllCategoriesAsync();
                Console.WriteLine("\nMövcud kateqoriyalar:");
                foreach (var cat in categories)
                {
                    Console.WriteLine($"{cat.Id}. {cat.Name}");
                }

                Console.Write("Kateqoriya ID: ");
                if (!int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    Console.WriteLine("Yanlış kateqoriya ID!");
                    return;
                }

                await _menuItemService.AddMenuItemAsync(name, price, categoryId);
                Console.WriteLine("Menu item uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task EditMenuItemAsync()
        {
            try
            {
                Console.Write("Düzəliş ediləcək menu item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Yanlış ID formatı!");
                    return;
                }

                Console.Write("Yeni ad: ");
                var name = Console.ReadLine();

                Console.Write("Yeni qiymət: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    Console.WriteLine("Yanlış qiymət formatı!");
                    return;
                }

                await _menuItemService.EditMenuItemAsync(id, name, price);
                Console.WriteLine("Menu item uğurla yeniləndi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task RemoveMenuItemAsync()
        {
            try
            {
                Console.Write("Silinəcək menu item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Yanlış ID formatı!");
                    return;
                }

                await _menuItemService.RemoveMenuItemAsync(id);
                Console.WriteLine("Menu item uğurla silindi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task ShowAllMenuItemsAsync()
        {
            try
            {
                var menuItems = await _menuItemService.GetAllMenuItemsAsync();
                
                if (menuItems.Count == 0)
                {
                    Console.WriteLine("Heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine("\n=== BÜTÜN MENU İTEMLƏR ===");
                Console.WriteLine($"{"ID",-5} {"Ad",-20} {"Kateqoriya",-15} {"Qiymət",-10}");
                Console.WriteLine(new string('-', 55));
                
                foreach (var item in menuItems)
                {
                    Console.WriteLine($"{item.Id,-5} {item.Name,-20} {item.Category?.Name,-15} {item.Price,-10:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task ShowMenuItemsByCategoryAsync()
        {
            try
            {
                // Kateqoriyaları göstər
                var categories = await _categoryService.GetAllCategoriesAsync();
                Console.WriteLine("\nMövcud kateqoriyalar:");
                foreach (var cat in categories)
                {
                    Console.WriteLine($"{cat.Id}. {cat.Name}");
                }

                Console.Write("Kateqoriya ID: ");
                if (!int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    Console.WriteLine("Yanlış kateqoriya ID!");
                    return;
                }

                var menuItems = await _menuItemService.GetMenuItemsByCategoryAsync(categoryId);
                
                if (menuItems.Count == 0)
                {
                    Console.WriteLine("Bu kateqoriyada heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine("\n=== MENU İTEMLƏR ===");
                Console.WriteLine($"{"ID",-5} {"Ad",-20} {"Kateqoriya",-15} {"Qiymət",-10}");
                Console.WriteLine(new string('-', 55));
                
                foreach (var item in menuItems)
                {
                    Console.WriteLine($"{item.Id,-5} {item.Name,-20} {item.Category?.Name,-15} {item.Price,-10:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task ShowMenuItemsByPriceRangeAsync()
        {
            try
            {
                Console.Write("Minimum qiymət: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal minPrice))
                {
                    Console.WriteLine("Yanlış qiymət formatı!");
                    return;
                }

                Console.Write("Maksimum qiymət: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal maxPrice))
                {
                    Console.WriteLine("Yanlış qiymət formatı!");
                    return;
                }

                var menuItems = await _menuItemService.GetMenuItemsByPriceRangeAsync(minPrice, maxPrice);
                
                if (menuItems.Count == 0)
                {
                    Console.WriteLine("Bu qiymət aralığında heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine("\n=== MENU İTEMLƏR ===");
                Console.WriteLine($"{"ID",-5} {"Ad",-20} {"Kateqoriya",-15} {"Qiymət",-10}");
                Console.WriteLine(new string('-', 55));
                
                foreach (var item in menuItems)
                {
                    Console.WriteLine($"{item.Id,-5} {item.Name,-20} {item.Category?.Name,-15} {item.Price,-10:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        private async Task SearchMenuItemsAsync()
        {
            try
            {
                Console.Write("Axtarış sözü: ");
                var searchTerm = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    Console.WriteLine("Axtarış sözü boş ola bilməz!");
                    return;
                }

                var menuItems = await _menuItemService.SearchMenuItemsAsync(searchTerm);
                
                if (menuItems.Count == 0)
                {
                    Console.WriteLine("Heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine("\n=== AXTARIŞ NƏTİCƏLƏRİ ===");
                Console.WriteLine($"{"ID",-5} {"Ad",-20} {"Kateqoriya",-15} {"Qiymət",-10}");
                Console.WriteLine(new string('-', 55));
                
                foreach (var item in menuItems)
                {
                    Console.WriteLine($"{item.Id,-5} {item.Name,-20} {item.Category?.Name,-15} {item.Price,-10:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }
    }
}

