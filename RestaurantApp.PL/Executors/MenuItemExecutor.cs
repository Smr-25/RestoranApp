using RestaurantApp.BBL.DTOs.Categories;
using RestaurantApp.BBL.DTOs.MenuItems;

namespace RestaurantApp.PL.Executors
{
    public class MenuItemExecutor
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public MenuItemExecutor(IMenuItemService menuItemService, ICategoryService categoryService, IMapper mapper)
        {
            _menuItemService = menuItemService;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task ExecuteAddMenuItemAsync()
        {
            Console.WriteLine("\n=== Yeni Menu Item Əlavə Et ===");
            Name:
            Console.Write("Ad daxil edin: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ad boş ola bilməz! Yenidən cəhd edin.");
                goto Name;
            }

            Price:
            Console.Write("Qiymət daxil edin: ");
            string priceInput = Console.ReadLine();
            if (!decimal.TryParse(priceInput, out decimal price))
            {
                Console.WriteLine("Yanlış qiymət formatı! Yenidən cəhd edin.");
                goto Price;
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            var categoryDtos = _mapper.Map<List<CategoryReturnDto>>(categories);
            
            Console.WriteLine("\nMövcud Kateqoriyalar:");
            Console.WriteLine(CategoryReturnDto.GetHeader());
            Console.WriteLine(CategoryReturnDto.GetSeparator());
            foreach (var cat in categoryDtos)
            {
                Console.WriteLine(cat);
            }

            CategoryId:
            Console.Write("\nKateqoriya ID daxil edin: ");
            string idInput = Console.ReadLine();
            if (!int.TryParse(idInput, out int categoryId))
            {
                Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                goto CategoryId;
            }

            try
            {
                await _menuItemService.AddMenuItemAsync(name, price, categoryId);
                Console.WriteLine("Menu item uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteEditMenuItemAsync()
        {
            Console.WriteLine("\n=== Menu Item-i Düzəliş Et ===");

            MenuItemId:
            Console.Write("Düzəliş ediləcək item nömrəsi: ");
            string menuItemIdInput = Console.ReadLine();
            if (!int.TryParse(menuItemIdInput, out int id))
            {
                Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                goto MenuItemId;
            }

            Name:
            Console.Write("Yeni ad: ");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ad boş ola bilməz! Yenidən cəhd edin.");
                goto Name;
            }

            Price:
            Console.Write("Yeni qiymət: ");
            string priceInput = Console.ReadLine();
            if (!decimal.TryParse(priceInput, out decimal price))
            {
                Console.WriteLine("Yanlış qiymət formatı! Yenidən cəhd edin.");
                goto Price;
            }

            try
            {
                await _menuItemService.EditMenuItemAsync(id, name, price);
                Console.WriteLine("Menu item uğurla yeniləndi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteRemoveMenuItemAsync()
        {
            Console.WriteLine("\n=== Menu Item-i Sil ===");

            MenuItemId:
            Console.Write("Silinəcək item nömrəsi: ");
            string menuItemIdInput = Console.ReadLine();
            if (!int.TryParse(menuItemIdInput, out int id))
            {
                Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                goto MenuItemId;
            }

            try
            {
                await _menuItemService.RemoveMenuItemAsync(id);
                Console.WriteLine("Menu item uğurla silindi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowAllMenuItemsAsync()
        {
            Console.WriteLine("\n=== Bütün Menu Item-lar ===");

            try
            {
                var items = await _menuItemService.GetAllMenuItemsAsync();
                var itemDtos = _mapper.Map<List<MenuItemReturnDto>>(items);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine(MenuItemReturnDto.GetHeader());
                Console.WriteLine(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowMenuItemsByCategoryAsync()
        {
            Console.WriteLine("\n=== Kateqoriyaya Görə Menu Item-lar ===");

            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                var categoryDtos = _mapper.Map<List<CategoryReturnDto>>(categories);

                Console.WriteLine("\nMövcud Kateqoriyalar:");
                Console.WriteLine(CategoryReturnDto.GetHeader());
                Console.WriteLine(CategoryReturnDto.GetSeparator());
                foreach (var cat in categoryDtos)
                {
                    Console.WriteLine(cat);
                }

                CategoryId:
                Console.Write("\nKateqoriya nömrəsini seçin: ");
                string categoryIdInput = Console.ReadLine();
                if (!int.TryParse(categoryIdInput, out int categoryId))
                {
                    Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                    goto CategoryId;
                }

                var items = await _menuItemService.GetMenuItemsByCategoryAsync(categoryId);
                var itemDtos = _mapper.Map<List<MenuItemReturnDto>>(items);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Bu kateqoriyada heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine(MenuItemReturnDto.GetHeader());
                Console.WriteLine(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteShowMenuItemsByPriceRangeAsync()
        {
            Console.WriteLine("\n=== Qiymət Aralığına Görə Menu Item-lar ===");

            MinPrice:
            Console.Write("Minimum qiymət: ");
            string minPriceInput = Console.ReadLine();
            if (!decimal.TryParse(minPriceInput, out decimal minPrice))
            {
                Console.WriteLine("Yanlış qiymət formatı! Yenidən cəhd edin.");
                goto MinPrice;
            }

            MaxPrice:
            Console.Write("Maksimum qiymət: ");
            string maxPriceInput = Console.ReadLine();
            if (!decimal.TryParse(maxPriceInput, out decimal maxPrice))
            {
                Console.WriteLine("Yanlış qiymət formatı! Yenidən cəhd edin.");
                goto MaxPrice;
            }

            try
            {
                var items = await _menuItemService.GetMenuItemsByPriceRangeAsync(minPrice, maxPrice);
                var itemDtos = _mapper.Map<List<MenuItemReturnDto>>(items);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Bu qiymət aralığında heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine(MenuItemReturnDto.GetHeader());
                Console.WriteLine(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteSearchMenuItemsAsync()
        {
            Console.WriteLine("\n=== Menu Item-lar Arasında Axtar ===");

            SearchTerm:
            Console.Write("Axtarış mətni daxil edin: ");
            string searchTerm = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                Console.WriteLine("Axtarış mətni boş ola bilməz! Yenidən cəhd edin.");
                goto SearchTerm;
            }

            try
            {
                var items = await _menuItemService.SearchMenuItemsAsync(searchTerm);
                var itemDtos = _mapper.Map<List<MenuItemReturnDto>>(items);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Heç bir uyğun menu item tapılmadı.");
                    return;
                }

                Console.WriteLine(MenuItemReturnDto.GetHeader());
                Console.WriteLine(MenuItemReturnDto.GetSeparator());
                foreach (var item in itemDtos)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }


        public async Task ShowMenuOperationsMenuAsync()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("   MENU ƏMƏLİYYATLARI");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("1. Yeni item əlavə et");
            Console.WriteLine("2. Item üzərində düzəliş et");
            Console.WriteLine("3. Item sil");
            Console.WriteLine("4. Bütün item-ları göstər");
            Console.WriteLine("5. Kateqoriyasına görə menu item-ları göstər");
            Console.WriteLine("6. Qiymət aralığına görə menu item-lar göstər");
            Console.WriteLine("7. Menu item-lar arasında ada görə axtarış et");
            Console.WriteLine("0. Əvvəlki menyuya qayıt");
            Console.WriteLine(new string('=', 50));
            Console.Write("Seçiminiz: ");
        }
    }
}

