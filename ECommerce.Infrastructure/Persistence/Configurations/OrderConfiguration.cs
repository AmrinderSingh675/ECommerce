
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Persistence.Configurations;

///having to manually register every single one in DbContext, I use ApplyConfigurationsFromAssembly.
///Instead of writing modelBuilder.ApplyConfiguration(new ProductConfiguration()); 
///for every entity, I just add one line inside DbContext class.
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.OrderNumber)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.TotalAmount)
               .HasPrecision(18, 2);
    }
}
