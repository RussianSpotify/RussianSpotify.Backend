using MassTransit;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Shared.Enums;
using RussianSpotify.API.Shared.Models.PaymentsEvents;
using RussianSpotify.Grpc.SubscriptionService.Data;

namespace RussianSpotify.Grpc.SubscriptionService.Consumers;

public class PaymentFailedConsumer : IConsumer<PaymentFailedEvent>
{
    private readonly ILogger<PaymentCreatedConsumer> _logger;
    private readonly IDbContext _dbContext;

    public PaymentFailedConsumer(ILogger<PaymentCreatedConsumer> logger, IDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        var message = context.Message;

        if (message is null)
        {
            _logger.LogCritical("Сообщение пришло пустым: {@message}", context.Message);
            return;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(context.CancellationToken);
        try
        {
            var subscription = await _dbContext.Subscriptions
                .FirstOrDefaultAsync(x => x.Id == message.SubscriptionId, context.CancellationToken);

            if (subscription is null)
            {
                _logger.LogCritical("Сообщение с идентификатором {messageId} не найдено", message.SubscriptionId);
                return;
            }

            subscription.Status = SubscriptionStatus.Failed;
            subscription.FailedReason = message.Reason;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(context.CancellationToken);
            _logger.LogCritical("Произошла ошибка при отмене подписки {exceptionMessage}", e.Message);
        }
    }
}