namespace RestaurantApp.DDL.Data.Configurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItem");
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(m => m.Name)
                .IsUnique();

            builder.Property(m => m.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(m => m.CategoryId)
                .IsRequired();

            builder.HasOne(m => m.Category)
                .WithMany()
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

        
            builder.HasData(
                
                new MenuItem { Id = 1, Name = "Çörək Səbəti", Price = 3.50m, CategoryId = 1 },
                new MenuItem { Id = 2, Name = "Pomidor Şorbası", Price = 5.00m, CategoryId = 1 },
                new MenuItem { Id = 3, Name = "Kükü", Price = 4.50m, CategoryId = 1 },
                new MenuItem { Id = 4, Name = "Zeytun", Price = 3.00m, CategoryId = 1 },
                new MenuItem { Id = 5, Name = "Pendir Seçimi", Price = 6.50m, CategoryId = 1 },
                
                
                new MenuItem { Id = 6, Name = "Toyuq Şiş", Price = 12.00m, CategoryId = 2 },
                new MenuItem { Id = 7, Name = "Lülə Kabab", Price = 15.00m, CategoryId = 2 },
                new MenuItem { Id = 8, Name = "Balıq Filesi", Price = 18.00m, CategoryId = 2 },
                new MenuItem { Id = 9, Name = "Plov", Price = 10.00m, CategoryId = 2 },
                new MenuItem { Id = 10, Name = "Biftek", Price = 20.00m, CategoryId = 2 },
                
              
                new MenuItem { Id = 11, Name = "Çoban Salatı", Price = 5.50m, CategoryId = 3 },
                new MenuItem { Id = 12, Name = "Sezar Salatı", Price = 7.00m, CategoryId = 3 },
                new MenuItem { Id = 13, Name = "Yunan Salatı", Price = 6.50m, CategoryId = 3 },
                new MenuItem { Id = 14, Name = "Mangal Salatı", Price = 6.00m, CategoryId = 3 },
                new MenuItem { Id = 15, Name = "Göyərti Salatı", Price = 4.50m, CategoryId = 3 },
                
               
                new MenuItem { Id = 16, Name = "Tiramisu", Price = 6.00m, CategoryId = 4 },
                new MenuItem { Id = 17, Name = "Şokolad Tortu", Price = 5.50m, CategoryId = 4 },
                new MenuItem { Id = 18, Name = "Baklava", Price = 4.00m, CategoryId = 4 },
                new MenuItem { Id = 19, Name = "Profiterol", Price = 5.00m, CategoryId = 4 },
                new MenuItem { Id = 20, Name = "Cheesecake", Price = 6.50m, CategoryId = 4 },
                
               
                new MenuItem { Id = 21, Name = "Kola", Price = 2.00m, CategoryId = 5 },
                new MenuItem { Id = 22, Name = "Portağal Şirəsi", Price = 3.50m, CategoryId = 5 },
                new MenuItem { Id = 23, Name = "Ayran", Price = 2.50m, CategoryId = 5 },
                new MenuItem { Id = 24, Name = "Türk Qəhvəsi", Price = 3.00m, CategoryId = 5 },
                new MenuItem { Id = 25, Name = "Çay", Price = 1.50m, CategoryId = 5 }
            );
        }
    }
}
