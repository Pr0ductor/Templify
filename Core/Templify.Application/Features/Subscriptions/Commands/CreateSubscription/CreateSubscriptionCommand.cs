using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Templify.Application.Features.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommand : IRequest<int>
{
    [Required]
    public int CourseId { get; set; }
    
    [Required]
    [Range(0, double.MaxValue)]
    public decimal PaidAmount { get; set; }
    
    [Required]
    [StringLength(50)]
    public string PaymentStatus { get; set; } = string.Empty;
    
    public DateTime? ExpirationDate { get; set; }
}
