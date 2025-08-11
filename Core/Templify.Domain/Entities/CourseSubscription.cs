using Templify.Domain.Common;

namespace Templify.Domain.Entities;

public class CourseSubscription : BaseAuditableEntity
{
    public int CourseId { get; set; }
    public int UserId { get; set; }
    public DateTime SubscriptionDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpirationDate { get; set; }
    public bool IsActive { get; set; } = true;
    public decimal PaidAmount { get; set; }
    public string PaymentStatus { get; set; } = string.Empty; // Pending, Completed, Failed, Refunded
    
    // Навигационные свойства
    public virtual Course Course { get; set; } = null!;
    public virtual AppUser User { get; set; } = null!;
}
