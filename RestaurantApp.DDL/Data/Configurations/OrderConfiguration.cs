using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Core.Models;
using System;

namespace RestaurantApp.DDL.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.Id);

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

            
            builder.HasData(
                new Order { Id = 1, TotalAmount = 35.50m, Date = new DateTime(2024, 1, 15, 12, 30, 0) },
                new Order { Id = 2, TotalAmount = 52.00m, Date = new DateTime(2024, 1, 15, 13, 15, 0) },
                new Order { Id = 3, TotalAmount = 28.50m, Date = new DateTime(2024, 1, 15, 14, 0, 0) },
                new Order { Id = 4, TotalAmount = 67.00m, Date = new DateTime(2024, 1, 16, 11, 45, 0) },
                new Order { Id = 5, TotalAmount = 41.50m, Date = new DateTime(2024, 1, 16, 12, 20, 0) }
            );
        }
    }
}
