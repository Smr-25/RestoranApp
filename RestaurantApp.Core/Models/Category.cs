
using RestaurantApp.DDL.Common;

namespace RestaurantApp.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public List<MenuItem> MenuItems { get; set; }

       
    }
}
