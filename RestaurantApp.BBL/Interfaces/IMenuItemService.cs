namespace RestaurantApp.BBL.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync(string name,decimal price,int categoryId);
        Task RemoveMenuItemAsync(int id);
        Task EditMenuItemAsync(int id, string name, decimal price);
        Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId);
        Task<List<MenuItem>> GetMenuItemsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<List<MenuItem>> SearchMenuItemsAsync(string searchTerm);
        Task<List<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem?> GetMenuItemByIdAsync(int id);
    }
}
