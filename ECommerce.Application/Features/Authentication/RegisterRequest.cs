
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.Features.Authentication;
public class RegisterRequest
{
    [Required]
    [MaxLength(200)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[69]\d{7}$", ErrorMessage = "Mobile number must be exactly 8 digits and start with 6 or 9.")]
    public string Mobile { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$*&])[A-Za-z\d!@#$*&]{8,12}$", ErrorMessage = "Password must be 8-12 characters and contain at least one uppercase letter, one number and one special character (!,@,#,$,*,&).")]
    public string Password { get; set; } = string.Empty;

    [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the Terms and Conditions.")]
    public bool AcceptTerms { get; set; }
}

