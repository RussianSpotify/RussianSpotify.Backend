#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Models;
using RussianSpotify.API.Shared.Enums;
using RussianSpotify.API.Shared.Interfaces;

#endregion

namespace RussianSpotify.API.Core.Requests.Subscription.SendEndSubscribeNotification;

/// <summary>
///     Обработчик для <see cref="SendEndSubscribeNotificationQuery" />
/// </summary>
public class SendEndSubscribeNotificationQueryHandler : IRequestHandler<SendEndSubscribeNotificationQuery>
{
    private const int NoticeInterval = -7;

    private readonly IExternalSubscriptionDbContext _externalSubscriptionContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDbContext _dbContext;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dateTimeProvider">Провайдер дат</param>
    /// <param name="externalSubscriptionContext">Контекст внешней бд по подпискам</param>
    /// <param name="dbContext">Контекст Бд</param>
    public SendEndSubscribeNotificationQueryHandler(
        IDateTimeProvider dateTimeProvider,
        IExternalSubscriptionDbContext externalSubscriptionContext,
        IDbContext dbContext)
    {
        _dateTimeProvider = dateTimeProvider;
        _externalSubscriptionContext = externalSubscriptionContext;
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task Handle(SendEndSubscribeNotificationQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var subscribes = await _externalSubscriptionContext.ExternalSubscriptions
            .Where(x => x.DateEnd.HasValue)
            .Where(x => x.Status == SubscriptionStatus.Active)
            .Where(x => x.DateEnd!.Value.Date == _dateTimeProvider.CurrentDate.Date.AddDays(NoticeInterval))
            .ToListAsync(cancellationToken);

        if (!subscribes.Any())
            return;

        var usernames = await _dbContext.Users.Join(
            subscribes,
            user => user.Id,
            subscribe => subscribe.UserId,
            (user, subscribe) => new UserData(user.Id, user.UserName, user.Email))
            .ToListAsync(cancellationToken);

        foreach (var subscribe in subscribes)
            await AddEmailNotificationAsync(subscribe, usernames, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task AddEmailNotificationAsync(
        ExternalSubscription subscribe,
        List<UserData> usernames,
        CancellationToken cancellationToken)
    {
        var userData = usernames.FirstOrDefault(x => x.Id == subscribe.UserId);

        if (userData == null)
            throw new EntityNotFoundException<User>($"Пользователь с идентификатором {subscribe.UserId} не найден");
        
        var placeholders = new Dictionary<string, string>
        {
            ["{username}"] = userData?.UserName ?? string.Empty,
        };

        var emailNotification = await EmailTemplateHelper
            .GetEmailNotificationAsync(
                placeholders: placeholders,
                template: Templates.SendEndSubscribeNotification,
                head: "Истекает срок подписки",
                emailTo: userData!.Email,
                cancellationToken: cancellationToken);

        await _dbContext.EmailNotifications.AddAsync(emailNotification, cancellationToken);
    }

    private record UserData(Guid Id, string UserName, string Email);
}