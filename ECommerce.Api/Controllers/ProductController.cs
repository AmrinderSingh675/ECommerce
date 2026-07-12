
using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Application.Features.Common;
using ECommerce.Application.Features.Order;
using ECommerce.Application.Features.Product;

namespace ECommerce.Api.Controllers;

[Route("api/product")]
[ApiController]
public class ProductController : ControllerBase
{
    //injecting the interfaces into constructor
    private readonly IProductService _productService;
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    //getting the list of products
    [HttpGet("getproducts")]
    public async Task<IActionResult> GetProducts([FromQuery] FilterRequest request)
    {
        //yet I did not implemented the paging
        var result = await _productService.SearchAsync(request);

        return Ok(result);
    }

    //getting the product detail by parameter id
    [HttpGet("getproduct")]
    public async Task<IActionResult> GetProduct(int id)
    {
        var result = await _productService.GetByIdAsync(id);
        
        return Ok(result);
    }
}