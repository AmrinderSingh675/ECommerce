
using AutoMapper;
using ECommerce.Application.Features.Common;
using ECommerce.Application.Features.Order;
using ECommerce.Application.Features.Product;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    public class OrderService : GenericService<Order, OrderRequest, OrderResponse>, IOrderService
    {
        private readonly IGenericRepository<OrderItem> _orderItemRepository;
        private readonly IGenericRepository<Order> _repository;
        private readonly IMapper _mapper;

        public OrderService(
            IGenericRepository<Order> repository,
            IMapper mapper,
            IGenericRepository<OrderItem> orderItemRepository) : base(repository, mapper)
        {
            _orderItemRepository = orderItemRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderResponse> AddOrderAsync(Guid userId, OrderRequest request)
        {
            request.UserId = userId;
            //Map the request to your main domain entity
            var order = _mapper.Map<Order>(request);

            //Execute business rule: Save order first to generate the DB Identity ID
            order.GSTAmount = request.GST;
            order.TotalAmount = request.Total;
            order.PayableAmount = request.Payable;
            order = await _repository.AddAsync(order);

            //Execute business rule: Generate custom Order Number using the generated ID
            order.OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{order.Id}";
            await _repository.UpdateAsync(order); // Update order with its new number

            //Execute business rule: Handle Child Order Items
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
