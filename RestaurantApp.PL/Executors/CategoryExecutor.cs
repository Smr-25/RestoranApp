using RestaurantApp.BBL.Dtos.Categories;

namespace RestaurantApp.PL.Executors;

public class CategoryExecutor
{
    private readonly ICategoryService _categoryService;

    public CategoryExecutor(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

        public async Task ExecuteAddCategoryAsync()
        {
            Console.WriteLine("\n=== Yeni Kateqoriya Əlavə Et ===");
            Name:
            Console.Write("Ad daxil edin: ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ad boş ola bilməz! Yenidən cəhd edin.");
                goto Name;
            }

            try
            {
                var dto = new CategoryCreateDto { Name = name };
                await _categoryService.AddCategoryAsync(dto);
                Console.WriteLine("Kateqoriya uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteEditCategoryAsync()
        {
            Console.WriteLine("\n=== Kateqoriya Düzəliş Et ===");

            CategoryId:
            Console.Write("Düzəliş ediləcək kateqoriya ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                goto CategoryId;
            }

            Name:
            Console.Write("Yeni ad: ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Ad boş ola bilməz! Yenidən cəhd edin.");
                goto Name;
            }

            try
            {
                var dto = new CategoryUpdateDto { Id = id, Name = name };
                await _categoryService.EditCategoryAsync(dto);
                Console.WriteLine("Kateqoriya uğurla yeniləndi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteRemoveCategoryAsync()
        {
            Console.WriteLine("\n=== Kateqoriya Sil ===");

            CategoryId:
            Console.Write("Silinəcək kateqoriya ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                goto CategoryId;
            }

            try
            {
                await _categoryService.RemoveCategoryAsync(id);
                Console.WriteLine("Kateqoriya uğurla silindi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }

    public async Task ExecuteShowAllCategoriesAsync()
    {
        Console.WriteLine("\n=== Bütün Kateqoriyalar ===");

        try
        {
            var categoryDtos = await _categoryService.GetAllCategoriesAsync();

            if (!categoryDtos.Any())
            {
                Console.WriteLine("Heç bir kateqoriya tapılmadı.");
                return;
            }

            Console.WriteLine(CategoryReturnDto.GetHeader());
            Console.WriteLine(CategoryReturnDto.GetSeparator());
            foreach (var category in categoryDtos)
            {
                Console.WriteLine(category);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Xəta: {ex.Message}");
        }
    }

        public async Task ExecuteShowCategoryByIdAsync()
        {
            Console.WriteLine("\n=== Kateqoriya Məlumatları ===");

            CategoryId:
            Console.Write("Kateqoriya ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                Console.WriteLine("Yanlış ID formatı! Yenidən cəhd edin.");
                goto CategoryId;
            }

            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    Console.WriteLine($"ID-si {id} olan kateqoriya tapılmadı.");
                    return;
                }

                Console.WriteLine(CategoryReturnDto.GetHeader());
                Console.WriteLine(CategoryReturnDto.GetSeparator());
                Console.WriteLine(category);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta: {ex.Message}");
            }
        }
    
    public void ShowCategoryOperationsMenuAsync()
    {
        Console.WriteLine("\n=== Kateqoriya Əməliyyatları ===");
        Console.WriteLine("1. Yeni Kateqoriya Əlavə Et");
        Console.WriteLine("2. Kateqoriya Düzəliş Et");
        Console.WriteLine("3. Kateqoriya Sil");
        Console.WriteLine("4. Bütün Kateqoriyaları Göstər");
        Console.WriteLine("5. ID-yə Görə Kateqoriya Göstər");
        Console.WriteLine("0. Çıxış");
        Console.Write("Seçiminiz: ");
    }
}