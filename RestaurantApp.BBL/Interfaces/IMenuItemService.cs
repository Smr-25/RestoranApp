namespace RestaurantApp.BBL.Interfaces
{
    public interface IMenuItemService
    {
        Task AddMenuItemAsync();
        Task RemoveMenuItemAsync();

        Task EditMenuItemAsync();
    }
}
