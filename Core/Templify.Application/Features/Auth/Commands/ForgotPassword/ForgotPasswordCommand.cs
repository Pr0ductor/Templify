using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Templify.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommand : IRequest<bool>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
