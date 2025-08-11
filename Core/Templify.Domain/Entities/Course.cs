using Templify.Domain.Common;

namespace Templify.Domain.Entities;

public class Course : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? VideoUrl { get; set; }
    public int Duration { get; set; } // в минутах
    public string Level { get; set; } = string.Empty; // Beginner, Intermediate, Advanced
    public bool IsPublished { get; set; } = false;
    public bool IsFeatured { get; set; } = false;
    public int Rating { get; set; } = 0;
    public int StudentsCount { get; set; } = 0;
    
    // Внешние ключи
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
    
    // Навигационные свойства
    public virtual AppUser Author { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<CourseSubscription> Subscriptions { get; set; } = new List<CourseSubscription>();
    public virtual ICollection<CourseModule> Modules { get; set; } = new List<CourseModule>();
}
