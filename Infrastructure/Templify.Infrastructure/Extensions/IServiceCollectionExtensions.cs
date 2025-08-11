using Templify.Application.Interfaces.Services;
using Templify.Infrastructure.Services;
using Templify.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Templify.Domain.Entities;

namespace Templify.Infrastructure.Extensions;

public static class IServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {
        services.AddServices();
        services.AddIdentity();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services
            .AddTransient<IAuthService, AuthService>()
            .AddTransient<IEmailService, EmailService>()
            .AddTransient<ICourseService, CourseService>()
            .AddTransient<ICategoryService, CategoryService>();
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}
