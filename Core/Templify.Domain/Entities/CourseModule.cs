using Templify.Domain.Common;

namespace Templify.Domain.Entities;

public class CourseModule : BaseAuditableEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    public int Duration { get; set; } // в минутах
    public bool IsPublished { get; set; } = false;
    
    // Внешний ключ
    public int CourseId { get; set; }
    
    // Навигационные свойства
    public virtual Course Course { get; set; } = null!;
    public virtual ICollection<CourseLesson> Lessons { get; set; } = new List<CourseLesson>();
}
