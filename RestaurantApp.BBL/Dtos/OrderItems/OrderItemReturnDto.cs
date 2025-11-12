namespace RestaurantApp.BBL.Dtos.OrderItems
{ 
    public class OrderItemReturnDto
    {
        public int Id { get; set; }
        public string MenuItemName { get; set; }
        public int Count { get; set; }

        public override string ToString()
        {
            return $"{Id,-10}{MenuItemName,-30}{Count,-10}";
        }

        public static string GetHeader()
        {
            return $"{"Nömrə",-10}{"Ad",-30}{"Say",-10}";
        }

        public static string GetSeparator()
        {
            return new string('-', 50);
        }
    }
}

