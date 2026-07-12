
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Repositories;
public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetOrderWithItemsAsync(int id);
}

