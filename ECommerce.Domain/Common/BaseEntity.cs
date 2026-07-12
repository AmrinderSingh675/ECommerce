namespace ECommerce.Domain.Common;

// The base entity inherited by all other entities to prevent repeating common fields.
public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; } 
    public bool IsDeleted { get; set; } = false;
}