using System.Text.Json;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Shared.Models.SubscriptionEvents;
using RussianSpotify.Grpc.SubscriptionService.Data;

namespace RussianSpotify.Grpc.SubscriptionService.Workers;

public class OutBoxDispatcherBackgroundService : BackgroundService
{
    private readonly ILogger<OutBoxDispatcherBackgroundService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IBus _bus;

    public OutBoxDispatcherBackgroundService(
        ILogger<OutBoxDispatcherBackgroundService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IBus bus)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await using var dbContext = _serviceScopeFactory
                    .CreateScope()
                    .ServiceProvider
                    .GetRequiredService<SubscriptionDbContext>();

                var messages = await dbContext.MessageOutboxes
                    .Where(message => !message.IsSent)
                    .Take(100)
                    .ToListAsync(stoppingToken);

                if (!messages.Any())
                    continue;

                foreach (var message in messages)
                {
                    var type = Type.GetType(message.Type);

                    if (type is null)
                    {
                        _logger.LogCritical("Не удалось найти тип {MessageType}", message.Type);
                        continue;
                    }
                    
                    var payload = JsonSerializer.Deserialize(message.Payload, type);

                    if (payload is null)
                    {
                        _logger.LogCritical(
                            "Не удалось десериализовать объект {MessagePayload} Type: {Type}",
                            message.Payload,
                            message.Type);

                        continue;
                    }
                    
                    var convertedPayload = payload switch
                    {
                        SubscriptionCreatedEvent subscriptionCreatedEvent => subscriptionCreatedEvent,
                        _ => throw new ArgumentOutOfRangeException()
                    };
                    
                    await _bus.Publish(convertedPayload, stoppingToken);
                }
                
                messages.ForEach(x => x.IsSent = true);
                await dbContext.SaveChangesAsync(stoppingToken);
                await Task.Delay(10000, stoppingToken);
            }
            catch (Exception e)
            {
                _logger.BeginScope(new Dictionary<string, object>()
                {
                    ["Service"] = nameof(OutBoxDispatcherBackgroundService),
                    ["Message"] = e.Message,
                    ["StackTrace"] = e.StackTrace ?? string.Empty,
                });
                _logger.LogError("Произошла ошибка при отправке сообщения");
            }
        }
    }
}