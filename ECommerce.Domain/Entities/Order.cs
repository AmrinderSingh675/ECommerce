using ECommerce.Domain.Common;

namespace ECommerce.Domain.Entities;
public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public string? OrderNumber { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = "832/16";
    public string City { get; set; } = "Kharar";
    public string State { get; set; } = "Punjab";
    public string Country { get; set; } = "IND";
    public string PostalCode { get; set; } = "140301";
    public decimal TotalAmount { get; set; }
    public decimal GSTAmount { get; set; }
    public decimal PayableAmount { get; set; }
    public string PaymentMethod { get; set; } = "Cash";
    public string PaymentStatus { get; set; } = "Paid";
    public string OrderStatus { get; set; } = "Pending";
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}