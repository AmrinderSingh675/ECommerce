
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Features.Common;
using ECommerce.Application.Features.Order;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ECommerce.Api.Controllers;

[Route("api/order")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : ControllerBase
{
    //injecting the interfaces into constructor
    private readonly IOrderService _orderService;
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("addorder")]
    public async Task<IActionResult> AddOrder(OrderRequest order)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return Unauthorized("User ID is missing or invalid.");
        }
        var result = await _orderService.AddOrderAsync(userId, order);
        return Ok();
    }

    [HttpPut("updateorder")]
    public async Task<IActionResult> UpdateOrder(OrderRequest order)
    {
        //var result = await _orderService.UpdateAsync(order.Id, order);

        return Ok();
    }

    [HttpGet("getorders")]
    public async Task<IActionResult> GetOrders([FromQuery] FilterRequest request)
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdString, out Guid userId))
        {
            return Unauthorized("User ID is missing or invalid.");
        }
        //yet I did not implemented the paging
        var result = await _orderService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("getorder")]
    public async Task<IActionResult> GetOrder(int id)
    {
        var result = await _orderService.GetByIdAsync(id);

        return Ok(result);
    }
}