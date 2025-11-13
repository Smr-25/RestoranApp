using RestaurantApp.BBL.Dtos.Categories;
using RestaurantApp.BBL.Dtos.MenuItems;
using RestaurantApp.PL.Helpers;

namespace RestaurantApp.PL.Executors
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

        public async Task ExecuteAddMenuItemAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Yeni Menu Item Əlavə Et ===");
            Name:
            ConsoleHelper.WritePrompt("Ad daxil edin: ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleHelper.WriteError("Ad boş ola bilməz! Yenidən cəhd edin.");
                goto Name;
            }

            Price:
            ConsoleHelper.WritePrompt("Qiymət daxil edin: ");
            string priceInput = Console.ReadLine()!;
            if (!decimal.TryParse(priceInput, out decimal price))
            {
                ConsoleHelper.WriteError("Yanlış qiymət formatı! Yenidən cəhd edin.");
                goto Price;
            }

            var categoryDtos = await _categoryService.GetAllCategoriesAsync();
            
            ConsoleHelper.WriteInfo("\nMövcud Kateqoriyalar:");
            ConsoleHelper.WriteInfo(CategoryReturnDto.GetHeader());
            ConsoleHelper.WriteInfo(CategoryReturnDto.GetSeparator());
            foreach (var cat in categoryDtos)
            {
                Console.WriteLine(cat);
            }

            CategoryId:
            ConsoleHelper.WritePrompt("\nKateqoriya ID daxil edin: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int categoryId))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto CategoryId;
            }

            try
            {
                var dto = new MenuItemCreateDto { Name = name, Price = price, CategoryId = categoryId };
                await _menuItemService.AddMenuItemAsync(dto);
                ConsoleHelper.WriteSuccess("Menu item uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteEditMenuItemAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Menu Item-i Düzəliş Et ===");

            MenuItemId:
            ConsoleHelper.WritePrompt("Düzəliş ediləcək item nömrəsi: ");
            string menuItemIdInput = Console.ReadLine()!;
            if (!int.TryParse(menuItemIdInput, out int id))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto MenuItemId;
            }

            Name:
            ConsoleHelper.WritePrompt("Yeni ad: ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleHelper.WriteError("Ad boş ola bilməz! Yenidən cəhd edin.");
                goto Name;
            }

            Price:
            ConsoleHelper.WritePrompt("Yeni qiymət: ");
            string priceInput = Console.ReadLine()!;
            if (!decimal.TryParse(priceInput, out decimal price))
            {
                ConsoleHelper.WriteError("Yanlış qiymət formatı! Yenidən cəhd edin.");
                goto Price;
            }

            var categoryDtos = await _categoryService.GetAllCategoriesAsync();
            
            ConsoleHelper.WriteInfo("\nMövcud Kateqoriyalar:");
            ConsoleHelper.WriteInfo(CategoryReturnDto.GetHeader());
            ConsoleHelper.WriteInfo(CategoryReturnDto.GetSeparator());
            foreach (var cat in categoryDtos)
            {
                Console.WriteLine(cat);
            }

            CategoryId:
            ConsoleHelper.WritePrompt("\nKateqoriya ID daxil edin: ");
            string categoryIdInput = Console.ReadLine()!;
            if (!int.TryParse(categoryIdInput, out int categoryId))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto CategoryId;
            }

            try
            {
                var dto = new MenuItemUpdateDto { Id = id, Name = name, Price = price, CategoryId = categoryId };
                await _menuItemService.EditMenuItemAsync(dto);
                ConsoleHelper.WriteSuccess("Menu item uğurla yeniləndi!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteRemoveMenuItemAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Menu Item-i Sil ===");

            MenuItemId:
            ConsoleHelper.WritePrompt("Silinəcək item nömrəsi: ");
            string menuItemIdInput = Console.ReadLine()!;
            if (!int.TryParse(menuItemIdInput, out int id))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto MenuItemId;
            }

            try
            {
                await _menuItemService.RemoveMenuItemAsync(id);
                ConsoleHelper.WriteSuccess("Menu item uğurla silindi!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowAllMenuItemsAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Bütün Menu Item-lar ===");

            try
            {
                var itemDtos = await _menuItemService.GetAllMenuItemsAsync();

                if (!itemDtos.Any())
                {
                    ConsoleHelper.WriteWarning("Heç bir menu item tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowMenuItemsByCategoryAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Kateqoriyaya Görə Menu Item-lar ===");

            try
            {
                var categoryDtos = await _categoryService.GetAllCategoriesAsync();

                ConsoleHelper.WriteInfo("\nMövcud Kateqoriyalar:");
                ConsoleHelper.WriteInfo(CategoryReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(CategoryReturnDto.GetSeparator());
                foreach (var cat in categoryDtos)
                {
                    Console.WriteLine(cat);
                }

                CategoryId:
                ConsoleHelper.WritePrompt("\nKateqoriya nömrəsini seçin: ");
                string categoryIdInput = Console.ReadLine()!;
                if (!int.TryParse(categoryIdInput, out int categoryId))
                {
                    ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                    goto CategoryId;
                }

                var itemDtos = await _menuItemService.GetMenuItemsByCategoryAsync(categoryId);

                if (!itemDtos.Any())
                {
                    ConsoleHelper.WriteWarning("Bu kateqoriyada heç bir menu item tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowMenuItemsByPriceRangeAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Qiymət Aralığına Görə Menu Item-lar ===");

            MinPrice:
            ConsoleHelper.WritePrompt("Minimum qiymət: ");
            string minPriceInput = Console.ReadLine()!;
            if (!decimal.TryParse(minPriceInput, out decimal minPrice))
            {
                ConsoleHelper.WriteError("Yanlış qiymət formatı! Yenidən cəhd edin.");
                goto MinPrice;
            }

            MaxPrice:
            ConsoleHelper.WritePrompt("Maksimum qiymət: ");
            string maxPriceInput = Console.ReadLine()!;
            if (!decimal.TryParse(maxPriceInput, out decimal maxPrice))
            {
                ConsoleHelper.WriteError("Yanlış qiymət formatı! Yenidən cəhd edin.");
                goto MaxPrice;
            }

            try
            {
                var itemDtos = await _menuItemService.GetMenuItemsByPriceRangeAsync(minPrice, maxPrice);

                if (!itemDtos.Any())
                {
                    ConsoleHelper.WriteWarning("Bu qiymət aralığında heç bir menu item tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteSearchMenuItemsAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Menu Item-lar Arasında Axtar ===");

            SearchTerm:
            ConsoleHelper.WritePrompt("Axtarış mətni daxil edin: ");
            string searchTerm = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                ConsoleHelper.WriteError("Axtarış mətni boş ola bilməz! Yenidən cəhd edin.");
                goto SearchTerm;
            }

            try
            {
                var itemDtos = await _menuItemService.SearchMenuItemsAsync(searchTerm);

                if (!itemDtos.Any())
                {
                    ConsoleHelper.WriteWarning("Heç bir uyğun menu item tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowMenuItemByIdAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Menu Item Məlumatları ===");

            MenuItemId:
            ConsoleHelper.WritePrompt("Menu Item ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto MenuItemId;
            }

            try
            {
                var item = await _menuItemService.GetMenuItemByIdAsync(id);
                if (item == null)
                {
                    ConsoleHelper.WriteWarning($"ID-si {id} olan menu item tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(MenuItemReturnDto.GetSeparator());
                Console.WriteLine(item);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }


        public void ShowMenuOperationsMenuAsync()
        {
            Console.WriteLine();
            ConsoleHelper.WriteHeader(new string('=', 50));
            ConsoleHelper.WriteHeader("   MENU ƏMƏLİYYATLARI");
            ConsoleHelper.WriteHeader(new string('=', 50));
            ConsoleHelper.WriteInfo("1. Yeni Menu Item Əlavə Et");
            ConsoleHelper.WriteInfo("2. Menu Item Düzəliş Et");
            ConsoleHelper.WriteInfo("3. Menu Item Sil");
            ConsoleHelper.WriteInfo("4. Bütün Menu Item-ları Göstər");
            ConsoleHelper.WriteInfo("5. Kateqoriyaya Görə Menu Item-lar");
            ConsoleHelper.WriteInfo("6. Qiymət Aralığına Görə Menu Item-lar");
            ConsoleHelper.WriteInfo("7. Menu Item-lar Arasında Axtar");
            ConsoleHelper.WriteInfo("8. ID-yə Görə Menu Item Göstər");
            ConsoleHelper.WriteInfo("0. Çıxış");
            ConsoleHelper.WritePrompt("Seçiminiz: ");
        }
    }
}
