using AutoMapper;
using ECommerce.Application.Features.Common;
using ECommerce.Application.Features.Product;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using System.Linq;

namespace ECommerce.Application.Services
{
    public class ProductService : GenericService<Product, ProductRequest, ProductResponse>, IProductService
    {
        public ProductService(IGenericRepository<Product> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        public async Task<IEnumerable<ProductResponse>> SearchAsync(FilterRequest request)
        {
            //yet I did not implemented the paging
            return await FindAsync(x => string.IsNullOrWhiteSpace(request.Key) || x.Name.Contains(request.Key));
        }
    }
}
