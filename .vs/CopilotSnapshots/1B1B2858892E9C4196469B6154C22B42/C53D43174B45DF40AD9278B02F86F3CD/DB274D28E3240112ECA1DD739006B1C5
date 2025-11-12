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
            
            builder.HasIndex(oi => oi.MenuItemId).IsUnique();
            
            builder.HasOne(oi=>oi.MenuItem)
                .WithOne(mi=>mi.OrderItem)
                .HasForeignKey<OrderItem>(oi=>oi.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);
   
        }
    }
}
