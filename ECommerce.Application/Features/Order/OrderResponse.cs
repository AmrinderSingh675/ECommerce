
namespace ECommerce.Application.Features.Order;
public record OrderResponse
{
    public string UserId { get; set; }
    public string OrderNumber { get; set; }
    public string ShippingAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }
    public string OrderStatus { get; set; }
}

