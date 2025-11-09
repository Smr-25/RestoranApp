using AutoMapper;
using RestaurantApp.BBL.DTOs;
using RestaurantApp.BBL.Interfaces;

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

        public async Task AddMenuItemAsync()
        {
            Console.WriteLine("\n=== Yeni Menu Item Əlavə Et ===");
            
            Console.Write("Ad daxil edin: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Qiymət daxil edin: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Yanlış qiymət formatı!");
                return;
            }

            var categories = await _categoryService.GetAllCategoriesAsync();
            Console.WriteLine("\nMövcud Kateqoriyalar:");
            foreach (var cat in categories)
            {
                Console.WriteLine($"{cat.Id}. {cat.Name}");
            }

            Console.Write("Kateqoriya ID daxil edin: ");
            if (!int.TryParse(Console.ReadLine(), out int categoryId))
            {
                Console.WriteLine("Yanlış ID formatı!");
                return;
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

        public async Task EditMenuItemAsync()
        {
            Console.WriteLine("\n=== Menu Item-i Düzəliş Et ===");
            
            Console.Write("Düzəliş ediləcək item nömrəsi: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Yanlış ID formatı!");
                return;
            }

            Console.Write("Yeni ad: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Yeni qiymət: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("Yanlış qiymət formatı!");
                return;
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

        public async Task RemoveMenuItemAsync()
        {
            Console.WriteLine("\n=== Menu Item-i Sil ===");
            
            Console.Write("Silinəcək item nömrəsi: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Yanlış ID formatı!");
                return;
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

        public async Task ShowAllMenuItemsAsync()
        {
            Console.WriteLine("\n=== Bütün Menu Item-lar ===");
            
            try
            {
                var items = await _menuItemService.GetAllMenuItemsAsync();
                var itemDtos = _mapper.Map<List<MenuItemDto>>(items);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine($"{"Nömrə",-10}{"Ad",-30}{"Kateqoriya",-20}{"Qiymət",-10}");
                Console.WriteLine(new string('-', 70));
                foreach (var item in itemDtos)
                {
                    Console.WriteLine($"{item.Id,-10}{item.Name,-30}{item.CategoryName,-20}{item.Price,-10:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ShowMenuItemsByCategoryAsync()
        {
            Console.WriteLine("\n=== Kateqoriyaya Görə Menu Item-lar ===");
            
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

                Console.WriteLine("\nMövcud Kateqoriyalar:");
                foreach (var cat in categoryDtos)
                {
                    Console.WriteLine($"{cat.Id}. {cat.Name}");
                }

                Console.Write("\nKateqoriya nömrəsini seçin: ");
                if (!int.TryParse(Console.ReadLine(), out int categoryId))
                {
                    Console.WriteLine("Yanlış ID formatı!");
                    return;
                }

                var items = await _menuItemService.GetMenuItemsByCategoryAsync(categoryId);
                var itemDtos = _mapper.Map<List<MenuItemDto>>(items);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Bu kateqoriyada heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine($"\n{"Nömrə",-10}{"Ad",-30}{"Kateqoriya",-20}{"Qiymət",-10}");
                Console.WriteLine(new string('-', 70));
                foreach (var item in itemDtos)
                {
                    Console.WriteLine($"{item.Id,-10}{item.Name,-30}{item.CategoryName,-20}{item.Price,-10:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ShowMenuItemsByPriceRangeAsync()
        {
            Console.WriteLine("\n=== Qiymət Aralığına Görə Menu Item-lar ===");
            
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

            try
            {
                var items = await _menuItemService.GetMenuItemsByPriceRangeAsync(minPrice, maxPrice);
                var itemDtos = _mapper.Map<List<MenuItemDto>>(items);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Bu qiymət aralığında heç bir menu item tapılmadı.");
                    return;
                }

                Console.WriteLine($"\n{"Nömrə",-10}{"Ad",-30}{"Kateqoriya",-20}{"Qiymət",-10}");
                Console.WriteLine(new string('-', 70));
                foreach (var item in itemDtos)
                {
                    Console.WriteLine($"{item.Id,-10}{item.Name,-30}{item.CategoryName,-20}{item.Price,-10:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task SearchMenuItemsAsync()
        {
            Console.WriteLine("\n=== Menu Item-lar Arasında Axtar ===");
            
            Console.Write("Axtarış mətni daxil edin: ");
            string searchTerm = Console.ReadLine() ?? "";

            try
            {
                var items = await _menuItemService.SearchMenuItemsAsync(searchTerm);
                var itemDtos = _mapper.Map<List<MenuItemDto>>(items);

                if (!itemDtos.Any())
                {
                    Console.WriteLine("Heç bir uyğun menu item tapılmadı.");
                    return;
                }

                Console.WriteLine($"\n{"Nömrə",-10}{"Ad",-30}{"Kateqoriya",-20}{"Qiymət",-10}");
                Console.WriteLine(new string('-', 70));
                foreach (var item in itemDtos)
                {
                    Console.WriteLine($"{item.Id,-10}{item.Name,-30}{item.CategoryName,-20}{item.Price,-10:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }
    }
}

