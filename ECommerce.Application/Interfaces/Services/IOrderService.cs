
using ECommerce.Application.Features.Common;
using ECommerce.Application.Features.Order;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;

//Created an entity-specific service to implement custom business logic methods.
public interface IOrderService : IGenericService<Order, OrderRequest, OrderResponse>
{
    Task<List<OrderResponse>> GetOrdersAsync(Guid userId);
    Task<OrderResponse> AddOrderAsync(Guid userId, OrderRequest request);
    Task<OrderResponse> GetOrderAsync(int id);
}

