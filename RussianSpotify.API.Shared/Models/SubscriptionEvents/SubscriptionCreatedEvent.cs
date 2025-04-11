namespace RussianSpotify.API.Shared.Models.SubscriptionEvents;

public class SubscriptionCreatedEvent
{
    public Guid UserId { get; set; }
    public Guid SubscriptionId { get; set; }
    public decimal Amount { get; set; }

    public Guid MessageId { get; set; }
}