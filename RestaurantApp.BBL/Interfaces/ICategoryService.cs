namespace RestaurantApp.BBL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryReturnDto>> GetAllCategoriesAsync();
        Task<CategoryReturnDto?> GetCategoryByIdAsync(int id);
        
        Task AddCategoryAsync(CategoryCreateDto dto);
        Task RemoveCategoryAsync(int id);
        Task EditCategoryAsync(CategoryUpdateDto dto);
    }
}

