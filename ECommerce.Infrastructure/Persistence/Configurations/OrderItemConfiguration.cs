

using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

///having to manually register every single one in DbContext, I use ApplyConfigurationsFromAssembly.
///Instead of writing modelBuilder.ApplyConfiguration(new ProductConfiguration()); 
///for every entity, I just add one line inside DbContext class.
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems");

        builder.Property(x => x.Price)
               .HasPrecision(18, 2);

        builder.Property(x => x.TotalAmount)
               .HasPrecision(18, 2);
    }
}