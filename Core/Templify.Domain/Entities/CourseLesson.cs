using Templify.Domain.Common;

namespace Templify.Domain.Entities;

public class CourseLesson : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string? VideoUrl { get; set; }
    public string? FileUrl { get; set; }
    public int Order { get; set; }
    public int Duration { get; set; } // в минутах
    public bool IsPublished { get; set; } = false;
    public bool IsFree { get; set; } = false;
    
    // Внешний ключ
    public int ModuleId { get; set; }
    
    // Навигационные свойства
    public virtual CourseModule Module { get; set; } = null!;
}
