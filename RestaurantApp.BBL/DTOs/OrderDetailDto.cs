namespace RestaurantApp.BBL.DTOs
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public int TotalItemCount { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public int Id { get; set; }
        public string MenuItemName { get; set; }
        public int Count { get; set; }
    }
}

