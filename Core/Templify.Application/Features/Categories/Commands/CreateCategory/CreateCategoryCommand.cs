using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Templify.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest<int>
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    
    [StringLength(100)]
    public string? Icon { get; set; }
    
    [StringLength(7)]
    public string? Color { get; set; }
    
    public int? ParentCategoryId { get; set; }
}
