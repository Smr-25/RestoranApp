namespace RestaurantApp.DDL.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(oi => oi.Id);

            builder.Property(oi => oi.Count)
                .IsRequired();

            builder.Property(oi => oi.OrderId)
                .IsRequired();

            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(oi => oi.MenuItem)
                .WithMany()
                .HasForeignKey("MenuItemId")
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasData(
               
                new { Id = 1, OrderId = 1, MenuItemId = 6, Count = 2 },
                new { Id = 2, OrderId = 1, MenuItemId = 2, Count = 1 },
                new { Id = 3, OrderId = 1, MenuItemId = 21, Count = 2 },
                
                
                new { Id = 4, OrderId = 2, MenuItemId = 7, Count = 1 },
                new { Id = 5, OrderId = 2, MenuItemId = 11, Count = 3 },
                new { Id = 6, OrderId = 2, MenuItemId = 23, Count = 2 },
                
                
                new { Id = 7, OrderId = 3, MenuItemId = 9, Count = 2 },
                new { Id = 8, OrderId = 3, MenuItemId = 15, Count = 1 },
                new { Id = 9, OrderId = 3, MenuItemId = 25, Count = 2 },
                
                
                new { Id = 10, OrderId = 4, MenuItemId = 8, Count = 1 },
                new { Id = 11, OrderId = 4, MenuItemId = 12, Count = 2 },
                new { Id = 12, OrderId = 4, MenuItemId = 18, Count = 1 },
                new { Id = 13, OrderId = 4, MenuItemId = 24, Count = 3 },
                
                
                new { Id = 14, OrderId = 5, MenuItemId = 10, Count = 2 },
                new { Id = 15, OrderId = 5, MenuItemId = 13, Count = 1 },
                new { Id = 16, OrderId = 5, MenuItemId = 16, Count = 2 },
                new { Id = 17, OrderId = 5, MenuItemId = 22, Count = 1 }
            );
        }
    }
}
