namespace RestaurantApp.BBL.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        
        Task AddCategoryAsync(string name);
        Task RemoveCategoryAsync(int id);
        Task EditCategoryAsync(int id, string name);
    }
}

