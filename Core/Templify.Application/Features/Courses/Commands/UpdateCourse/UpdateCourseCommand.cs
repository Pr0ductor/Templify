using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Templify.Application.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommand : IRequest<bool>
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [StringLength(500)]
    public string ShortDescription { get; set; } = string.Empty;
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    
    [StringLength(500)]
    public string? ImageUrl { get; set; }
    
    [StringLength(500)]
    public string? VideoUrl { get; set; }
    
    [Required]
    [Range(1, int.MaxValue)]
    public int Duration { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Level { get; set; } = string.Empty;
    
    [Required]
    public int CategoryId { get; set; }
    
    public bool IsPublished { get; set; }
    public bool IsFeatured { get; set; }
}
