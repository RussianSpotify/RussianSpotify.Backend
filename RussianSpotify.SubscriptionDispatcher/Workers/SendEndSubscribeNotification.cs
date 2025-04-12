using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Grpc.SubscriptionService.Features.Services;

namespace RussianSpotify.SubscriptionDispatcher.Workers;

public class SendEndSubscribeNotification : IWorker
{
    private readonly ILogger<SendEndSubscribeNotification> _logger;
    private readonly ISubscribeService _subscribeService;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="logger">Логгер</param>
    /// <param name="subscribeService"></param>
    public SendEndSubscribeNotification(
        ILogger<SendEndSubscribeNotification> logger,
        ISubscribeService subscribeService)
    {
        _logger = logger;
        _subscribeService = subscribeService;
    }

    /// <inheritdoc />
    public async Task RunAsync()
    {
        _logger.LogInformation("Send notification for subscribe...");
    }
}