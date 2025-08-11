using MediatR;

namespace Templify.Application.Features.Subscriptions.Queries.GetUserSubscriptions;

public class GetUserSubscriptionsQuery : IRequest<IEnumerable<UserSubscriptionDto>>
{
    public bool? IsActive { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class UserSubscriptionDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public string? CourseImageUrl { get; set; }
    public string AuthorName { get; set; } = string.Empty;
    public DateTime SubscriptionDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsActive { get; set; }
    public decimal PaidAmount { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public int ProgressPercentage { get; set; }
}
