using MassTransit;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Shared.Enums;
using RussianSpotify.API.Shared.Models.PaymentsEvents;
using RussianSpotify.Grpc.SubscriptionService.Data;

namespace RussianSpotify.Grpc.SubscriptionService.Consumers;

public class PaymentCreatedConsumer : IConsumer<PaymentCreatedEvent>
{
    private readonly ILogger<PaymentCreatedConsumer> _logger;
    private readonly IDbContext _dbContext;

    public PaymentCreatedConsumer(ILogger<PaymentCreatedConsumer> logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<PaymentCreatedEvent> context)
    {
        var message = context.Message;

        if (message == null)
        {
            _logger.LogCritical("Сообщение пришло пустым");
            return;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(context.CancellationToken);
        try
        {
            var subscription = await _dbContext.Subscriptions
                .FirstOrDefaultAsync(x => x.Id == message.SubscriptionId, context.CancellationToken);

            if (subscription == null)
            {
                _logger.LogCritical("Подписки не найдена с идентификатором {Id}", message.SubscriptionId);
                return;
            }

            subscription.Status = SubscriptionStatus.Active;
            
            await _dbContext.SaveChangesAsync(context.CancellationToken);
            await transaction.CommitAsync(context.CancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(context.CancellationToken);
            
            _logger.LogCritical("Не удалось поменять статус подписки {ExceptionMessage}", e.Message);
        }
    }
}