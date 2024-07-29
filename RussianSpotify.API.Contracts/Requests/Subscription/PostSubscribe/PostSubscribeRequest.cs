namespace RussianSpotify.Contracts.Requests.Subscription.PostSubscribe;

/// <summary>
/// Запрос для оформления подписки
/// </summary>
public class PostSubscribeRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    protected PostSubscribeRequest(PostSubscribeRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        SubscriptionLength = request.SubscriptionLength;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostSubscribeRequest()
    {
    }

    public int SubscriptionLength { get; set; }
}