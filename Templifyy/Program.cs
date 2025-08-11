using Templify.Application.Extensions;
using Templify.Infrastructure.Extensions;
using Templify.Persistence.Extensions;
using Templify.Shared.Constants;
using Templify.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Templify.Persistence.Contexts;
using Templify.Domain.Entities;
using Templifyy.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add layers
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);

// Настройка Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.LogoutPath = "/Auth/Logout";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Add custom middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Database migration and seeding
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    
    // Migrate database
    context.Database.Migrate();
    
    // Create roles if they don't exist
    if (!await roleManager.RoleExistsAsync(AppConstants.Roles.Admin))
    {
        await roleManager.CreateAsync(new IdentityRole(AppConstants.Roles.Admin));
    }
    if (!await roleManager.RoleExistsAsync(AppConstants.Roles.Author))
    {
        await roleManager.CreateAsync(new IdentityRole(AppConstants.Roles.Author));
    }
    if (!await roleManager.RoleExistsAsync(AppConstants.Roles.User))
    {
        await roleManager.CreateAsync(new IdentityRole(AppConstants.Roles.User));
    }
    
    // Create admin user if it doesn't exist
    var adminEmail = "admin@templify.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new IdentityUser
        {
            UserName = "admin",
            Email = adminEmail,
            EmailConfirmed = true
        };
        
        var result = await userManager.CreateAsync(adminUser, "Admin123!");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, AppConstants.Roles.Admin);
            
            // Create AppUser for admin
            var appUser = new AppUser
            {
                IdentityId = adminUser.Id,
                UserName = "admin",
                Email = adminEmail,
                FirstName = "Администратор",
                LastName = "Системы",
                Role = UserRole.Admin,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
            
            context.AppUsers.Add(appUser);
            await context.SaveChangesAsync();
        }
    }
}

app.Run();