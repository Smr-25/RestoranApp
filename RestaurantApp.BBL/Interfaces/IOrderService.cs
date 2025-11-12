namespace RestaurantApp.BBL.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderCreateDto dto);
        Task RemoveOrderAsync(int id);

        Task<List<OrderReturnDto>> GetOrdersByDateAsync(DateTime date);

        Task<OrderReturnDto?> GetOrderByIdAsync(int id);

        Task<List<OrderReturnDto>> GetOrdersByPriceIntervalAsync(decimal minPrice, decimal maxPrice);
        
        Task<List<OrderReturnDto>> GetAllOrdersAsync();
        
        Task<List<OrderReturnDto>> GetOrdersByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
