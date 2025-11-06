namespace RestaurantApp.BBL.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync();
        Task RemoveOrderAsync();

        Task GetOrderByDateAsync();

        Task GetOrderByNoAsync();

        Task GetOrdersByPriceIntervalAsync();
    }
}
