using RestaurantApp.Core.Common;
namespace RestaurantApp.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
