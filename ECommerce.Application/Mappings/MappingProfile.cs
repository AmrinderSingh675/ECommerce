using AutoMapper;
using ECommerce.Application.Features.Authentication;
using ECommerce.Application.Features.Order;
using ECommerce.Application.Features.Product;
using ECommerce.Domain.Entities;
using System.ComponentModel;
using System;

namespace ECommerce.Application.MappingProfiles
{
    //in future we can use the different profile class for the different module or entity
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderResponse>().ForMember(
                    dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));
            CreateMap<OrderRequest, Order>().ReverseMap();

            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<ProductRequest, Product>().ReverseMap();

            CreateMap<OrderItem, OrderItemRequest>().ReverseMap();
            CreateMap<OrderItemResponse, OrderItem>().ReverseMap();
        }
    }
}
