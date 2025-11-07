namespace RestaurantApp.BBL.Interfaces
{
    using RestaurantApp.Core.Models;

    public interface IMenuItemService
    {
        Task AddMenuItemAsync(MenuItem menuItem);
        Task RemoveMenuItemAsync(int id);

        Task EditMenuItemAsync(int id, MenuItem updatedMenuItem);
    }
}
