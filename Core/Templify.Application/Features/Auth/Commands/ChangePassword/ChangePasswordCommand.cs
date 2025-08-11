using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Templify.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordCommand : IRequest<bool>
{
    [Required]
    public string CurrentPassword { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string NewPassword { get; set; } = string.Empty;
    
    [Required]
    [Compare("NewPassword")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
