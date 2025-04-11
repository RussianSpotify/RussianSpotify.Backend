using RussianSpotify.API.Shared.Domain.Abstractions;

namespace RussianSpotify.API.PaymentService.Domain.Entities;

/// <summary>
/// Платеж
/// </summary>
public class Payment : BaseEntity, ITimeTrackable, ISoftDeletable
{
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public Guid SubscriptionId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
}