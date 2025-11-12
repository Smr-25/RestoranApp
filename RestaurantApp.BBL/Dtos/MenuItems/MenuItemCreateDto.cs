namespace RestaurantApp.BBL.Dtos.MenuItems
{
    public class MenuItemCreateDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
