
using ECommerce.Application.Features.Common;
using ECommerce.Application.Features.Order;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;

public interface IOrderService : IGenericService<Order, OrderRequest, OrderResponse>
{
    Task<OrderResponse> AddOrderAsync(Guid userId, OrderRequest request);
}

