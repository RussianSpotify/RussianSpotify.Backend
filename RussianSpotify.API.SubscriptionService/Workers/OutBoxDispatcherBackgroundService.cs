using System.Text.Json;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Shared.Models.SubscriptionEvents;
using RussianSpotify.Grpc.SubscriptionService.Data;
using RussianSpotify.Grpc.SubscriptionService.Domain.Entities;

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
                await using var scope = _serviceScopeFactory.CreateAsyncScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<SubscriptionDbContext>();

                var messages = await dbContext.MessageOutboxes
                    .Where(m => !m.IsSent)
                    .Take(100)
                    .ToListAsync(stoppingToken);

                if (messages.Count == 0)
                {
                    await Task.Delay(2000, stoppingToken);
                    continue;
                }

                var publishTasks = messages
                    .Select(message => HandleMessageAsync(message, stoppingToken))
                    .ToList();

                await Task.WhenAll(publishTasks);

                messages.ForEach(m => m.IsSent = true);
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка в OutboxDispatcher");
            }
        }
    }

    private async Task HandleMessageAsync(MessageOutbox message, CancellationToken ct)
    {
        try
        {
            var type = Type.GetType(message.Type);
            if (type == null)
            {
                _logger.LogWarning("Не удалось найти тип: {Type}", message.Type);
                return;
            }

            var payload = JsonSerializer.Deserialize(message.Payload, type);
            if (payload == null)
            {
                _logger.LogWarning("Payload пуст: {Payload}", message.Payload);
                return;
            }

            switch (payload)
            {
                case SubscriptionCreatedEvent created:
                    await _bus.Publish(created, ct);
                    break;

                default:
                    _logger.LogWarning("Неизвестный тип события: {Type}", type);
                    break;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обработке сообщения {Id}", message.Id);
        }
    }
}