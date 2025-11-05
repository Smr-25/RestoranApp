using RestaurantApp.DDL.Common;

namespace RestaurantApp.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public MenuItem MenuItem { get; set; }

        public int Count { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

    }
}
