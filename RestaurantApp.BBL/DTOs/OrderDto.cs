namespace RestaurantApp.BBL.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Date { get; set; }
        public int TotalItemCount { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

        public override string ToString()
        {
            return $"{Id,-10}{TotalAmount,-15:C}{TotalItemCount,-15}{Date,-20:yyyy-MM-dd HH:mm}";
        }

        public string ToDetailedString()
        {
            var result = $"\nNömrə: {Id}\n";
            result += $"Məbləğ: {TotalAmount:C}\n";
            result += $"Item Sayı: {TotalItemCount}\n";
            result += $"Tarix: {Date:yyyy-MM-dd HH:mm}\n";
            
            if (OrderItems.Any())
            {
                result += "\nSifariş Item-ları:\n";
                result += OrderItemDto.GetHeader() + "\n";
                result += OrderItemDto.GetSeparator() + "\n";
                foreach (var item in OrderItems)
                {
                    result += item + "\n";
                }
            }
            
            return result;
        }

        public static string GetHeader()
        {
            return $"{"Nömrə",-10}{"Məbləğ",-15}{"Item Sayı",-15}{"Tarix",-20}";
        }

        public static string GetSeparator()
        {
            return new string('-', 60);
        }
    }
}

