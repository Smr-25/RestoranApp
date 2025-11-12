namespace RestaurantApp.BBL.DTOs.Categories
{
    // Get üçün - göstərmək məqsədilə
    public class CategoryReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id,-10}{Name,-30}";
        }

        public static string GetHeader()
        {
            return $"{"Nömrə",-10}{"Ad",-30}";
        }

        public static string GetSeparator()
        {
            return new string('-', 40);
        }
    }

 
}


