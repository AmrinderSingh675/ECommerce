using AutoMapper;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Application
{
    //registers the Application layer services and register all AutoMapper profiles found there(so we do not have to manually register the mapping profile)
    public static class DependencyInjection
    {
        //register all AutoMapper profiles found there
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //registers the class into dependencies
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}