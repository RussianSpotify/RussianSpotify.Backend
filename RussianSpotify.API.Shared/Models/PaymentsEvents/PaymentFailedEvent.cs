namespace RussianSpotify.API.Shared.Models.PaymentsEvents;

public class PaymentFailedEvent
{
    public Guid SubscriptionId { get; set; }
    public string Reason { get; set; }
}