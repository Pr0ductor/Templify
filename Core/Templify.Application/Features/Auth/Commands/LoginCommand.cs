using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Templify.Application.Features.Auth.Commands;

public class LoginCommand : IRequest<AuthResult>
{
    [Required]
    public string Login { get; set; } = string.Empty; // Email или UserName
    
    [Required]
    public string Password { get; set; } = string.Empty;
    
    public bool RememberMe { get; set; } = false;
}

public class LoginResult
{
    public bool Succeeded { get; set; }
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public List<string> Errors { get; set; } = new();
}
