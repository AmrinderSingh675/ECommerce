
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel;
using System.Reflection.Emit;

namespace ECommerce.Infrastructure.Persistence.Configurations;

///having to manually register every single one in DbContext, I use ApplyConfigurationsFromAssembly.
///Instead of writing modelBuilder.ApplyConfiguration(new ProductConfiguration()); 
///for every entity, I just add one line inside DbContext class.
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(200);

        //builder.Property(x => x.Price)
        //       .HasPrecision(18, 2);
        //    builder.HasData(
        //        new Product
        //        {
        //            Name = "Apple iPhone 1",
        //            Price = 100,
        //            IsActive = true,
        //            ImageUrl = "images/iphone16.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new Product
        //        {
        //            Name = "Apple iPhone 2",
        //            Price = 200,
        //            IsActive = true,
        //            ImageUrl = "images/galaxy-s25.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new Product
        //        {
        //            Name = "Apple iPhone 3",
        //            Price = 300,
        //            IsActive = true,
        //            ImageUrl = "images/oneplus13.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new Product
        //        {
        //            Name = "Apple iPhone 4",
        //            Price = 400,
        //            IsActive = true,
        //            ImageUrl = "images/iphone16.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new Product
        //        {
        //            Name = "Apple iPhone 5",
        //            Price = 500,
        //            IsActive = true,
        //            ImageUrl = "images/galaxy-s25.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new Product
        //        {
        //            Name = "Apple iPhone 6",
        //            Price = 600,
        //            IsActive = true,
        //            ImageUrl = "images/oneplus13.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new Product
        //        {
        //            Name = "Apple iPhone 7",
        //            Price = 700,
        //            IsActive = true,
        //            ImageUrl = "images/iphone16.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new Product
        //        {
        //            Name = "Apple iPhone 8",
        //            Price = 800,
        //            IsActive = true,
        //            ImageUrl = "images/galaxy-s25.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new Product
        //        {
        //            Name = "Apple iPhone 9",
        //            Price = 900,
        //            IsActive = true,
        //            ImageUrl = "images/oneplus13.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        },
        //        new Product
        //        {
        //            Name = "Apple iPhone 10",
        //            Price = 1000,
        //            IsActive = true,
        //            ImageUrl = "images/oneplus13.jpg",
        //            CreatedAt = DateTime.UtcNow,
        //            IsDeleted = false
        //        }
        //    );
    }
}