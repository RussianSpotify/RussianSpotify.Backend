using RussianSpotify.API.Core.Requests.Subscription.GetSubscription;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.Subscriptions;

/// <summary>
/// Тест для <see cref="GetSubscriptionQueryHandler"/>
/// </summary>
public class GetSubscriptionQueryHandlerTest : UnitTestBase
{
    /// <summary>
    /// Обработчик должен ответить что метод был отработан без ошибок
    /// </summary>
    [Fact]
    public async Task Handle_ShouldVerifyMethod()
    {
        var request = new GetSubscriptionQuery();
        var handler = new GetSubscriptionQueryHandler(
            SubscriptionService.Object,
            UserContext.Object);

        await handler.Handle(request, default);
    }
}