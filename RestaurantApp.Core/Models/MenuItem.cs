using RestaurantApp.DDL.Common;

namespace RestaurantApp.Core.Models
{
    public class MenuItem : BaseEntity
    {

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}
