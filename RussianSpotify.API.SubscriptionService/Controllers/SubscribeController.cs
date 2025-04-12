using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RussianSpotify.Grpc.SubscriptionService.Features.Requests;
using RussianSpotify.Grpc.SubscriptionService.Features.Responses;
using RussianSpotify.Grpc.SubscriptionService.Features.Services;

namespace RussianSpotify.Grpc.SubscriptionService.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SubscribeController : ControllerBase
{
    private readonly ISubscribeService _subscribeService;

    public SubscribeController(ISubscribeService subscribeService)
    {
        _subscribeService = subscribeService;
    }
    
    /// <summary>
    ///     Эндпоинт, отвечающий за подписку пользователя
    /// </summary>
    /// <param name="request"><see cref="SubscriptionRequest" />, содержащий информацию о запросе</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Всё хорошо</response>
    /// <response code="400">Ошибка во ввёдённых данных</response>
    /// <response code="409">Конфликт с состоянием сущностей</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpPost("subscribe")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task SubscribeAsync([FromBody] SubscriptionRequest request, CancellationToken cancellationToken)
        => await _subscribeService.SubscribeAsync(request, cancellationToken);
    
    /// <summary>
    ///     Эндпоинт, отвечающий за получение информации о подписке пользователя
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns><see cref="GetSubscriptionResponse" />, содержащий информацию о подписке пользователя</returns>
    /// <response code="200">Всё хорошо</response>
    /// <response code="500">Внутренняя ошибка сервера</response>
    [HttpGet("getSubscribeInfo")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<GetSubscriptionResponse> GetInfoAsync(CancellationToken cancellationToken)
        => await _subscribeService.GetSubscriptionAsync(cancellationToken);
}