namespace RestaurantApp.BBL.DTOs
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return $"{Id,-10}{Name,-30}{CategoryName,-20}{Price,-10:C}";
        }

        public static string GetHeader()
        {
            return $"{"Nömrə",-10}{"Ad",-30}{"Kateqoriya",-20}{"Qiymət",-10}";
        }

        public static string GetSeparator()
        {
            return new string('-', 70);
        }
    }
}