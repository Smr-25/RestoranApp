using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantApp.Core.Models;

namespace RestaurantApp.DDL.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        void IEntityTypeConfiguration<Category>.Configure(EntityTypeBuilder<Category> builder)
        {
          
        }
    }
}
