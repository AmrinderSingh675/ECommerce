using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.Features.Authentication;
using ECommerce.Application.Interfaces.Services;
using System.ComponentModel.DataAnnotations;
using ECommerce.Application.Common.Exceptions;

namespace ECommerce.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthorizationsService _service;
    public AuthController(IAuthorizationsService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _service.RegisterAsync(request);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _service.LoginAsync(request.Email, request.Password);

        return Ok(result);
    }
}