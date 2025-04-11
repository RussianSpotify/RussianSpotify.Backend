using System.Text.Json;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Shared.Enums;
using RussianSpotify.API.Shared.Exceptions.SubscriptionExceptions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.Shared.Models.SubscriptionEvents;
using RussianSpotify.Grpc.SubscriptionService.Data;
using RussianSpotify.Grpc.SubscriptionService.Domain.Entities;
using RussianSpotify.Grpc.SubscriptionService.Features.Requests;
using RussianSpotify.Grpc.SubscriptionService.Features.Responses;

namespace RussianSpotify.Grpc.SubscriptionService.Features.Services;

public class SubscribeService : ISubscribeService
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDbContext _dbContext;
    private readonly ILogger<SubscribeService> _logger;
    private readonly IUserContext _userContext;

    public SubscribeService(
        ILogger<SubscribeService> logger,
        IDbContext dbContext,
        IDateTimeProvider dateTimeProvider, 
        IUserContext userContext)
    {
        _logger = logger;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
        _userContext = userContext;
    }

    public async Task SubscribeAsync(SubscriptionRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ArgumentNullException.ThrowIfNull(_userContext.CurrentUserId, nameof(_userContext));
        
        if (request.SubscribeLength < 1)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Subscription length must be greater than 0."));

        var dateSpan = new TimeSpan(request.SubscribeLength * 30, 0, 0, 0);
        var currentDateTime = _dateTimeProvider.CurrentDate;

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            if (await _dbContext.Subscriptions.AnyAsync(x => x.Status == SubscriptionStatus.Pending && x.UserId == _userContext.CurrentUserId, cancellationToken))
                throw new SubscriptionConflictException("Subscription already exists in status pending.");
            
            var subscription = await _dbContext.Subscriptions
                .Where(x => x.Status == SubscriptionStatus.Active && x.UserId == _userContext.CurrentUserId)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefaultAsync(i => i.UserId == _userContext.CurrentUserId, cancellationToken);
            
            var newSubscription = new Subscription
            {
                DateStart = subscription != null
                    ? subscription.DateEnd
                    : currentDateTime,
                
                DateEnd = subscription != null
                    ? subscription.DateEnd + dateSpan
                    :  currentDateTime + dateSpan,
                
                UserId = _userContext.CurrentUserId.Value,
                Status = SubscriptionStatus.Pending,
            };
            
            await _dbContext.Subscriptions.AddAsync(newSubscription, cancellationToken);
            await _dbContext.MessageOutboxes.AddAsync(new MessageOutbox
            {
                Payload = JsonSerializer.Serialize(new SubscriptionCreatedEvent
                {
                    MessageId = Guid.NewGuid(),
                    UserId = _userContext.CurrentUserId.Value,
                    SubscriptionId = subscription?.Id ?? newSubscription.Id,
                    //TODO тут бы сделать отдельную таблицу где будут хранится тариф.планы
                    Amount = (decimal)(Random.Shared.NextDouble() * 100)
                }),
                Type = typeof(SubscriptionCreatedEvent).AssemblyQualifiedName!,
            }, cancellationToken);
            
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.BeginScope(new Dictionary<string, object>()
            {
                ["Method"] = nameof(SubscribeAsync),
                ["UserId"] = _userContext.CurrentUserId.Value,
                ["Exception"] = e.Message,
                ["StackTrace"] = e.StackTrace ?? string.Empty,
            });
            _logger.LogError("Произошла ошибка при оформлении подписки");
        }
    }

    public async Task<GetSubscriptionResponse> GetSubscriptionAsync(CancellationToken cancellationToken)
    {
        var subscription = await _dbContext.Subscriptions
            .Where(x => x.Status == SubscriptionStatus.Active && x.UserId == _userContext.CurrentUserId)
            .OrderBy(x => x.CreatedAt)
            .FirstOrDefaultAsync(i => i.UserId == _userContext.CurrentUserId, cancellationToken);

        if (subscription?.DateStart is null || subscription.DateEnd is null)
            return new GetSubscriptionResponse
            {
                StartDate = null,
                EndDate = null
            };

        var getSubscriptionResponse = new GetSubscriptionResponse
        {
            StartDate = subscription.DateStart.Value,
            EndDate = subscription.DateEnd.Value,
            Status = subscription.Status.ToString()
        };

        return getSubscriptionResponse;
    }
}