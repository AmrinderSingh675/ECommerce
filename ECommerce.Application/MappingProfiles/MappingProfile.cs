using AutoMapper;
using ECommerce.Application.Features.Authentication;
using ECommerce.Application.Features.Order;
using ECommerce.Application.Features.Product;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderResponse>();
            CreateMap<OrderRequest, Order>();

            CreateMap<Product, ProductResponse>();
            CreateMap<ProductRequest, Product>();

            CreateMap<OrderItem, OrderItemRequest>();
            CreateMap<OrderItemRequest, OrderItem>();
        }
    }
}
