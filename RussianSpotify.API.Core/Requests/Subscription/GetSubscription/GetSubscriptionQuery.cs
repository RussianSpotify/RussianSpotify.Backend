#region

using MediatR;
using RussianSpotify.Contracts.Requests.Subscription.GetSubscription;

#endregion

namespace RussianSpotify.API.Core.Requests.Subscription.GetSubscription;

/// <summary>
///     Запрос на получение <see cref="GetSubscriptionResponse" />
/// </summary>
public class GetSubscriptionQuery : IRequest<GetSubscriptionResponse>
{
}