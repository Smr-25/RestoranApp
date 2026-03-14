using RestaurantApp.BBL.Dtos.Categories;
using RestaurantApp.PL.Helpers;

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
            ConsoleHelper.WriteHeader("\n=== Yeni Kateqoriya Əlavə Et ===");
            Name:
            ConsoleHelper.WritePrompt("Ad daxil edin: ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleHelper.WriteError("Ad boş ola bilməz! Yenidən cəhd edin.");
                goto Name;
            }

            try
            {
                var dto = new CategoryCreateDto { Name = name };
                await _categoryService.AddCategoryAsync(dto);
                ConsoleHelper.WriteSuccess("Kateqoriya uğurla əlavə edildi!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteEditCategoryAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Kateqoriya Düzəliş Et ===");

            CategoryId:
            ConsoleHelper.WritePrompt("Düzəliş ediləcək kateqoriya ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto CategoryId;
            }

            Name:
            ConsoleHelper.WritePrompt("Yeni ad: ");
            string name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                ConsoleHelper.WriteError("Ad boş ola bilməz! Yenidən cəhd edin.");
                goto Name;
            }

            try
            {
                var dto = new CategoryUpdateDto { Id = id, Name = name };
                await _categoryService.EditCategoryAsync(dto);
                ConsoleHelper.WriteSuccess("Kateqoriya uğurla yeniləndi!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

        public async Task ExecuteRemoveCategoryAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Kateqoriya Sil ===");

            CategoryId:
            ConsoleHelper.WritePrompt("Silinəcək kateqoriya ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto CategoryId;
            }

            try
            {
                await _categoryService.RemoveCategoryAsync(id);
                ConsoleHelper.WriteSuccess("Kateqoriya uğurla silindi!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }

    public async Task ExecuteShowAllCategoriesAsync()
    {
        ConsoleHelper.WriteHeader("\n=== Bütün Kateqoriyalar ===");

        try
        {
            var categoryDtos = await _categoryService.GetAllCategoriesAsync();

            if (!categoryDtos.Any())
            {
                ConsoleHelper.WriteWarning("Heç bir kateqoriya tapılmadı.");
                return;
            }

            ConsoleHelper.WriteInfo(CategoryReturnDto.GetHeader());
            ConsoleHelper.WriteInfo(CategoryReturnDto.GetSeparator());
            foreach (var category in categoryDtos)
            {
                Console.WriteLine(category);
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.WriteError($"Xəta: {ex.Message}");
        }
    }

        public async Task ExecuteShowCategoryByIdAsync()
        {
            ConsoleHelper.WriteHeader("\n=== Kateqoriya Məlumatları ===");

            CategoryId:
            ConsoleHelper.WritePrompt("Kateqoriya ID: ");
            string idInput = Console.ReadLine()!;
            if (!int.TryParse(idInput, out int id))
            {
                ConsoleHelper.WriteError("Yanlış ID formatı! Yenidən cəhd edin.");
                goto CategoryId;
            }

            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    ConsoleHelper.WriteWarning($"ID-si {id} olan kateqoriya tapılmadı.");
                    return;
                }

                ConsoleHelper.WriteInfo(CategoryReturnDto.GetHeader());
                ConsoleHelper.WriteInfo(CategoryReturnDto.GetSeparator());
                Console.WriteLine(category);
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Xəta: {ex.Message}");
            }
        }
    
    public void ShowCategoryOperationsMenuAsync()
    {
        ConsoleHelper.WriteHeader("\n=== Kateqoriya Əməliyyatları ===");
        ConsoleHelper.WriteInfo("1. Yeni Kateqoriya Əlavə Et");
        ConsoleHelper.WriteInfo("2. Kateqoriya Düzəliş Et");
        ConsoleHelper.WriteInfo("3. Kateqoriya Sil");
        ConsoleHelper.WriteInfo("4. Bütün Kateqoriyaları Göstər");
        ConsoleHelper.WriteInfo("5. ID-yə Görə Kateqoriya Göstər");
        ConsoleHelper.WriteInfo("0. Çıxış");
        ConsoleHelper.WritePrompt("Seçiminiz: ");
    }
}