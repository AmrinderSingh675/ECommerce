
using ECommerce.Application.Features.Product;
using ECommerce.Application.Features.Common;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;

//Created an entity-specific service to implement custom business logic methods.
public interface IProductService : IGenericService<Product, ProductRequest, ProductResponse>
{
    Task<IEnumerable<ProductResponse>> SearchAsync(FilterRequest request);
}

