
namespace ECommerce.Application.Features.Order;
public record OrderRequest
{
    public Guid UserId { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal Total { get; set; }
    public decimal GST { get; set; }
    public decimal Payable { get; set; }
    public List<OrderItemRequest> Items { get; set; }
}

