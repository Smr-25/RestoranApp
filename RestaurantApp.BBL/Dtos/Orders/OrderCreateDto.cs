using RestaurantApp.BBL.Dtos.OrderItems;

namespace RestaurantApp.BBL.Dtos.Orders
{
    public class OrderCreateDto
    {
        public List<OrderItemCreateDto> OrderItems { get; set; } = new List<OrderItemCreateDto>();
    }
}
