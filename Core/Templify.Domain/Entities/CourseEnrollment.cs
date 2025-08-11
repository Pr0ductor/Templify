using Templify.Domain.Common;

namespace Templify.Domain.Entities;

public class CourseEnrollment : BaseEntity
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrolledAt { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    // Navigation properties
    public virtual AppUser User { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
}
