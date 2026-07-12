using AutoMapper;
using ECommerce.Application.Features.Common;
using ECommerce.Application.Features.Product;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using System.Linq;

namespace ECommerce.Application.Services
{
    //Created an entity-specific service to implement custom business logic methods.
    public class ProductService : GenericService<Product, ProductRequest, ProductResponse>, IProductService
    {
        public ProductService(IGenericRepository<Product> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        public async Task<IEnumerable<ProductResponse>> SearchAsync(FilterRequest request)
        {
            //yet I did not implemented the paging and search from the front end
            return await FindAsync(x => string.IsNullOrWhiteSpace(request.Key) || x.Name.Contains(request.Key));
        }
    }
}
