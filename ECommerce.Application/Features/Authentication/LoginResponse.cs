namespace ECommerce.Application.Features.Authentication;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public bool Success { get; set; }

    public string? Message { get; set; }

    public int StatusCode { get; set; }
}
