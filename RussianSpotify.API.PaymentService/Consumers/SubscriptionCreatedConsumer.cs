using MassTransit;
using RussianSpotify.API.PaymentService.Data;
using RussianSpotify.API.PaymentService.Domain.Entities;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.Shared.Models.PaymentsEvents;
using RussianSpotify.API.Shared.Models.SubscriptionEvents;

namespace RussianSpotify.API.PaymentService.Consumers;

/// <summary>
/// Consumer на создание подписки
/// </summary>
public class SubscriptionCreatedConsumer : IConsumer<SubscriptionCreatedEvent>
{
    private readonly ILogger<SubscriptionCreatedConsumer> _logger;
    private readonly IBus _bus;
    private readonly IDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SubscriptionCreatedConsumer(
        ILogger<SubscriptionCreatedConsumer> logger,
        IBus bus,
        IDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _bus = bus;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task Consume(ConsumeContext<SubscriptionCreatedEvent> context)
    {
        var message = context.Message;

        if (message == null)
        {
            _logger.LogWarning("SubscriptionCreatedEvent пришел без сообщения");
            return;
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(context.CancellationToken);
        try
        {
            var payment = new Payment
            {
                UserId = message.UserId,
                Amount = message.Amount,
                SubscriptionId = message.SubscriptionId,
                CreatedAt = _dateTimeProvider.CurrentDate,
                UpdatedAt = _dateTimeProvider.CurrentDate,
            };
            
            await _dbContext.Payments.AddAsync(payment, context.CancellationToken);
            await _dbContext.SaveChangesAsync(context.CancellationToken);
            
            await _bus.Publish(new PaymentCreatedEvent
            {
                SubscriptionId = message.SubscriptionId,
            }, context.CancellationToken);
            
            await transaction.CommitAsync(context.CancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(context.CancellationToken);
            
            await _bus.Publish(new PaymentFailedEvent
            {
                SubscriptionId = message.SubscriptionId,
                Reason = e.Message,
            });
            
            _logger.LogError("Произошла ошибка при создании оплаты {MessageException}", e.Message);
        }
    }
}