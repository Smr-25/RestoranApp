using RestaurantApp.BBL.DTOs.OrderItems;

namespace RestaurantApp.BBL.DTOs.Orders
{
    public class OrderCreateDto
    {
        public List<OrderItemCreateDto> OrderItems { get; set; } = new List<OrderItemCreateDto>();
    }
}
