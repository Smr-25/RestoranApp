using RestaurantApp.DDL.Common;

namespace RestaurantApp.Core.Models
{
    public class Order : BaseEntity
    {
        public List<OrderItem> OrderItems { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime Date { get; set; }
    }
}
