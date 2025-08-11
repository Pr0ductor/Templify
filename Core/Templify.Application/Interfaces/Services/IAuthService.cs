using Templify.Application.Features.Auth.Commands;

namespace Templify.Application.Interfaces.Services;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterCommand command);
    Task<AuthResult> LoginAsync(LoginCommand command);
    Task<bool> LogoutAsync();
    Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task<bool> ConfirmEmailAsync(string userId, string token);
    Task<bool> ForgotPasswordAsync(string email);
    Task<bool> ResetPasswordAsync(string email, string token, string newPassword);
}

public class AuthResult
{
    public bool Succeeded { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<string> Errors { get; set; } = new();
    public string? Token { get; set; }
}
