
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.Price)
               .HasPrecision(18, 2);

    //    builder.HasData(
    //        new Product
    //        {
    //            Id = 1,
    //            Name = "Apple iPhone 16",
    //            Price = 89999,
    //            IsActive = true,
    //            ImageUrl = "images/iphone16.jpg",
    //            CreatedAt = DateTime.UtcNow,
    //            IsDeleted = false
    //        },
    //        new Product
    //        {
    //            Id = 2,
    //            Name = "Samsung Galaxy S25",
    //            Price = 79999,
    //            IsActive = true,
    //            ImageUrl = "images/galaxy-s25.jpg",
    //            CreatedAt = DateTime.UtcNow,
    //            IsDeleted = false
    //        },
    //        new Product
    //        {
    //            Id = 3,
    //            Name = "OnePlus 13",
    //            Price = 64999,
    //            IsActive = true,
    //            ImageUrl = "images/oneplus13.jpg",
    //            CreatedAt = DateTime.UtcNow,
    //            IsDeleted = false
    //        }
    //    );
    }
}
