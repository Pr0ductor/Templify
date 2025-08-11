using Templify.Domain.Common;
using Templify.Shared.Enums;

namespace Templify.Domain.Entities;

public class AppUser : BaseAuditableEntity
{
    public string IdentityId { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfileImage { get; set; }
    public string? Description { get; set; }
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Навигационные свойства
    public virtual IdentityUser IdentityUser { get; set; } = null!;
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<CourseSubscription> Subscriptions { get; set; } = new List<CourseSubscription>();
}

public enum UserRole
{
    User = 0,
    Author = 1,
    Admin = 2
}
