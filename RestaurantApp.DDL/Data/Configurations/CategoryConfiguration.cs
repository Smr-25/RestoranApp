namespace RestaurantApp.DDL.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
        
            builder.ToTable("Category");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn(6);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasMany(c => c.MenuItems)
                .WithOne(m => m.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasData(
                new Category { Id = 1, Name = "Başlanğıclar" },
                new Category { Id = 2, Name = "Əsas yeməklər" },
                new Category { Id = 3, Name = "Salatlar" },
                new Category { Id = 4, Name = "Desertlər" },
                new Category { Id = 5, Name = "İçkilər" }
            );
        }
    }
}
