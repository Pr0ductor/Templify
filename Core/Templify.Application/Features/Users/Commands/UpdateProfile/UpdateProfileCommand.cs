using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Templify.Application.Features.Users.Commands.UpdateProfile;

public class UpdateProfileCommand : IRequest<bool>
{
    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;
    
    [StringLength(1000)]
    public string? Description { get; set; }
    
    [StringLength(500)]
    public string? ProfileImage { get; set; }
}
