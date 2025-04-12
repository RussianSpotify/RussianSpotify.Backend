using RussianSpotify.API.Shared.Domain.Abstractions;

namespace RussianSpotify.Grpc.SubscriptionService.Domain.Entities;

public class MessageOutbox : BaseEntity
{
    public string Payload { get; set; }
    public bool IsSent { get; set; }

    public string Type { get; set; }
}