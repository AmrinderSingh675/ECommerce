
namespace ECommerce.Application.Features.Authentication;

public class RegisterResponse
{
    public bool Success { get; set; }

    public string? Message { get; set; }

    public int StatusCode { get; set; }
}