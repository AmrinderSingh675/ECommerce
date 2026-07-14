
using AutoMapper;
using ECommerce.Application.Features.Common;
using ECommerce.Application.Features.Order;
using ECommerce.Application.Features.Product;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Services
{
    ////Created an entity-specific service to implement custom business logic methods.
    public class OrderService : GenericService<Order, OrderRequest, OrderResponse>, IOrderService
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IGenericRepository<Order> repository,
            IOrderRepository orderRepository,
            IMapper mapper,
            IGenericRepository<OrderItem> orderItemRepository) : base(repository, mapper)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponse>> GetOrdersAsync(Guid userId)
        {
            var result = await _orderRepository.FindAsync(order => order.UserId == userId);
            var orders = _mapper.Map<List<OrderResponse>>(result);
            return orders;
        }

        public async Task<OrderResponse> GetOrderAsync(int id)
        {
            var result = await _orderRepository.GetOrderWithItemsAsync(id);
            var order = _mapper.Map<OrderResponse>(result);
            return order;
        }

        public async Task<OrderResponse> AddOrderAsync(Guid userId, OrderRequest request)
        {
            request.UserId = userId;
            //Map the request to your main domain entity
            var order = _mapper.Map<Order>(request);

            //Save order first to generate the DB Identity ID
            //order.GSTAmount = request.GSTAmount;
            //order.TotalAmount = request.TotalAmount;
            //order.PayableAmount = request.PayableAmount;
            order = await _orderRepository.AddAsync(order);

            //Generate custom Order Number using the generated ID
            order.OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{order.Id}";
            await _orderRepository.UpdateAsync(order); // Update order with its new number

            //Handle Child Order Items
            if (request.Items != null && request.Items.Any())
            {
                var orderItems = new List<OrderItem>();
                foreach (var itemRequest in request.Items)
                {
                    var orderItem = _mapper.Map<OrderItem>(itemRequest);
                    orderItem.TotalAmount = (itemRequest.Price * itemRequest.Quantity);
                    orderItem.OrderId = order.Id; // Link child to parent
                    orderItems.Add(orderItem);
                }
                await _orderItemRepository.AddRangeAsync(orderItems);
            }
            return _mapper.Map<OrderResponse>(order);
        }
    }
}
