
using ECommerce.Application.Features.Product;
using ECommerce.Application.Features.Common;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Interfaces.Services;
public interface IProductService : IGenericService<Product, ProductRequest, ProductResponse>
{
    Task<IEnumerable<ProductResponse>> SearchAsync(FilterRequest request);
}

