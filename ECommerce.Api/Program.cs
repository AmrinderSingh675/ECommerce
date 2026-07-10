
using ECommerce.Api.Middleware;
using ECommerce.Application;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Identity;
using ECommerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registers the Application layer services and register all AutoMapper profiles found there(so we do not have to manually register the mapping profile)
builder.Services.AddApplication();

//registers all the Infrastructure layer dependencies
builder.Services.AddInfrastructure(builder.Configuration);

// Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Identity Options
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;

    options.User.RequireUniqueEmail = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
//for now I commented it
//// Forwarded Headers (Useful for IIS, Nginx, Azure, Cloudflare)
//builder.Services.Configure<ForwardedHeadersOptions>(options =>
//{
//    options.ForwardedHeaders =  ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
//    // Configure KnownProxies/KnownNetworks in production.
//    options.KnownNetworks.Clear();
//    options.KnownProxies.Clear();
//});

// Global Rate Limiter(It will handle the load of api from the client per IP)
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            return RateLimitPartition.GetFixedWindowLimiter(
                ip,
                _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromMinutes(1),
                    QueueLimit = 0,
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                });
    });
    options.OnRejected = async (context, token) =>
    {
        const int retryAfterSeconds = 60;
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        context.HttpContext.Response.Headers["Retry-After"] = retryAfterSeconds.ToString();
        await context.HttpContext.Response.WriteAsJsonAsync(
            new
            {
                StatusCode = StatusCodes.Status429TooManyRequests,
                Message = "Too many requests. Please try again later.",
                RetryAfter = retryAfterSeconds
            },
            cancellationToken: token);
    };
});

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Exception Middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors("AngularPolicy");

// Forwarded Headers (for now I commented it)
// app.UseForwardedHeaders();
// Rate Limiting
app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();