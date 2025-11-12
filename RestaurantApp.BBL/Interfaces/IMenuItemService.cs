namespace RestaurantApp.BBL.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync(MenuItemCreateDto dto);
        Task RemoveMenuItemAsync(int id);
        Task EditMenuItemAsync(MenuItemUpdateDto dto);
        Task<List<MenuItemReturnDto>> GetMenuItemsByCategoryAsync(int categoryId);
        Task<List<MenuItemReturnDto>> GetMenuItemsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<List<MenuItemReturnDto>> SearchMenuItemsAsync(string searchTerm);
        Task<List<MenuItemReturnDto>> GetAllMenuItemsAsync();
        Task<MenuItemReturnDto?> GetMenuItemByIdAsync(int id);
    }
}
