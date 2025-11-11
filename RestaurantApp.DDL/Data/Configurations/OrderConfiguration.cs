﻿namespace RestaurantApp.DDL.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .UseIdentityColumn(6);

            builder.Property(o => o.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.Date)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
