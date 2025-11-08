namespace RestaurantApp.BBL.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(MenuItem menuItem, int count);
        Task RemoveOrderAsync(int id);

        Task<Order?> GetOrderByDateAsync(DateTime date);

        Task<Order?> GetOrderByIdAsync(int id);

        Task<List<Order>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice);
        
        Task<List<Order>> GetAllOrdersAsync();
        
        Task<List<Order>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
