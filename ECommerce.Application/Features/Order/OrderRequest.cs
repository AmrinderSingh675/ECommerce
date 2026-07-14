
namespace ECommerce.Application.Features.Order;
public record OrderRequest
{
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal GSTAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public List<OrderItemRequest> Items { get; set; }
}

