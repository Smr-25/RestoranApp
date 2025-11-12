namespace RestaurantApp.BBL.Dtos.MenuItems
{
    public class MenuItemUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
