
namespace ECommerce.Application.Features.Order;
public record OrderItemResponse
{
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
}

