using MediatR;
using System.ComponentModel.DataAnnotations;
using Templify.Shared.Enums;

namespace Templify.Application.Features.Auth.Commands;

public class RegisterCommand : IRequest<AuthResult>
{
    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string UserName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string? FirstName { get; set; }
    
    [Required]
    [StringLength(50)]
    public string? LastName { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
    
    [Required]
    public UserRole Role { get; set; }
    
    public bool RememberMe { get; set; }
}
