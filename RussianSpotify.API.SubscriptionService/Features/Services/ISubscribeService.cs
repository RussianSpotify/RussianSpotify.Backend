using RussianSpotify.Grpc.SubscriptionService.Features.Requests;
using RussianSpotify.Grpc.SubscriptionService.Features.Responses;

namespace RussianSpotify.Grpc.SubscriptionService.Features.Services;

public interface ISubscribeService
{
    /// <summary>
    /// Оформление подписки
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>-</returns>
    public Task SubscribeAsync(SubscriptionRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    ///     Получить информацию по подписке пользователя
    /// </summary>
    /// <returns><see cref="GetSubscriptionResponse" />, содержащая информацию о подписке пользователя</returns>
    public Task<GetSubscriptionResponse> GetSubscriptionAsync(CancellationToken cancellationToken);
    
    
}