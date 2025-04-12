namespace RussianSpotify.API.Shared.Models.PaymentsEvents;

public class PaymentCreatedEvent
{
    public Guid SubscriptionId { get; set; }
}