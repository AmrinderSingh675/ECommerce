
using ECommerce.Application.Exceptions;
using ECommerce.Application.Features.Authentication;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Services;

public class AuthenticationsService : IAuthorizationsService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ITokenService _tokenService;

    public AuthenticationsService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse> LoginAsync(string email, string password)
    {
        var user = new ApplicationUser();
        if (email.Contains("@"))
        {
            user = await _userManager.FindByEmailAsync(email);
        }
        else
        {
            user = await _userManager.Users.FirstOrDefaultAsync(u => u.Mobile == email);
        }
        
        if (user == null)
        {
            throw new UnauthorizedException("Invalid email/mobile or password");
        }

        //Check if the user is already locked out using UserManager
        if (await _userManager.IsLockedOutAsync(user))
        {
            throw new LockedException("Account locked due to too many failed attempts. Try again in 1 minute.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);
        //Set the 3rd parameter (lockoutOnFailure) to true
        if (result.IsLockedOut)
        {
            throw new LockedException("Account locked due to too many failed attempts. Try again in 1 minute.");
        }
        if (!result.Succeeded)
        {
            throw new UnauthorizedException("Invalid email/mobile or password");
        }

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault();

        var token = _tokenService.GenerateToken(user.Id.ToString(), user.Email, role);
        return new LoginResponse
        {
            Success = true,
            Token = token,
            Message = "Login successful",
            StatusCode = StatusCodes.Status200OK
        };
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = new ApplicationUser();

        existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
             throw new Exception("User already exists");


        existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Mobile == request.Email);
        if (existingUser != null)
            throw new Exception("User already exists");

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Mobile = request.Mobile,
            LockoutEnabled = true
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(",", result.Errors.Select(x => x.Description)));
        }

        await _userManager.SetLockoutEnabledAsync(user, true);

        // Default role
        const string roleName = "Customer";

        // Create role if not exists
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        // Assign role to user
        await _userManager.AddToRoleAsync(user, roleName);

        //var token = _tokenService.GenerateToken(user.Id, user.Email, roleName);
        return new RegisterResponse
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Login successful",
        };
    }
}