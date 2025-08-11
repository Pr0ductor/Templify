using System.Security.Claims;
using Templify.Shared.Constants;

namespace Templifyy.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetUserId(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static string? GetUserName(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Name);
    }

    public static string? GetUserEmail(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Email);
    }

    public static string? GetUserRole(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Role);
    }

    public static bool IsInRole(this ClaimsPrincipal user, string role)
    {
        return user.IsInRole(role);
    }

    public static bool IsAdmin(this ClaimsPrincipal user)
    {
        return user.IsInRole(AppConstants.Roles.Admin);
    }

    public static bool IsAuthor(this ClaimsPrincipal user)
    {
        return user.IsInRole(AppConstants.Roles.Author);
    }

    public static bool IsUser(this ClaimsPrincipal user)
    {
        return user.IsInRole(AppConstants.Roles.User);
    }
}
