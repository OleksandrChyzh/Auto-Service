using System.Text;
using BusinessLogic;
using BusinessLogic.Services;
using BusinessLogic.Interfaces;
using DAL;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

namespace API;

public static class ServiceExtensions
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("AppDbContext"));
        });

        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IMasterRepository, MasterRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IMasterService, MasterService>();
        services.AddScoped<IOrderService, BusinessLogic.Services.OrderService>();
        services.AddScoped<IReviewService, ReviewService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IServiceService, ServiceService>();


        services.AddAutoMapper(typeof(MappingProfile));
        services.AddControllers();
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();

        return services.AddAuthentication(configuration).AddAuthorization();
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Enter **only** your token below.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer", }, },
                    Array.Empty<string>()
                },
            });

            c.MapType<DateOnly>(() => new OpenApiSchema
            {
                Type = "string",
                Format = "date",
                Example = new OpenApiString("2025-04-18"),
            });

            c.MapType<TimeSpan>(() => new OpenApiSchema
            {
                Type = "string",
            });
        });

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = configuration["Jwt:Key"] ?? throw new ArgumentException("Jwt:Key is missing");
        var bytes = Encoding.UTF8.GetBytes(key);

        services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(bytes),
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync($"Unauthorized: {context.Error}");
                    },
                    OnAuthenticationFailed = async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Authentication failed.");
                    },
                };
            });

        return services;
    }
}