
using ECommerce.Application.Features.Authentication;
namespace ECommerce.Application.Interfaces.Services;

public interface IAuthorizationsService
{
    Task<LoginResponse> LoginAsync(string email, string password);
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
}

