using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;
public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    public bool IsActive { get; set; }
}