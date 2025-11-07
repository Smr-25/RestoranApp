using RestaurantApp.Core.Models;

namespace RestaurantApp.BBL.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync(MenuItem menuItem);
        Task RemoveMenuItemAsync(int id);
        Task EditMenuItemAsync(int id, MenuItem updatedMenuItem);
        Task<List<MenuItem>> GetMenuItemsByCategoryAsync(int categoryId);
        Task<List<MenuItem>> GetMenuItemsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<List<MenuItem>> SearchMenuItemsAsync(string searchTerm);
        Task<List<MenuItem>> GetAllMenuItemsAsync();
        Task<MenuItem?> GetMenuItemByIdAsync(int id);
    }
}
