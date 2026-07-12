
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;

//Created an entity-specific repository to implement custom business logic methods.
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetOrderWithItemsAsync(int id);
}

