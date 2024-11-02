using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RussianSpotify.API.Core.Requests.Chat.GetChats;
using RussianSpotify.API.Core.Requests.Chat.GetStory;
using RussianSpotify.Contracts.Requests.Chat.GetChats;
using RussianSpotify.Contracts.Requests.Chat.GetStory;

namespace RussianSpotify.API.WEB.Controllers;

/// <summary>
/// Контроллер чата
/// </summary>
[Route("api/[controller]/")]
[Authorize]
public class ChatController : ControllerBase
{
    /// <summary>
    /// Получить чаты, для админа
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("GetChats")]
    public async Task<GetChatsResponse> GetChatsAsync(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetChatsQuery(), cancellationToken);

    /// <summary>
    /// Получить историю чата
    /// </summary>
    [HttpGet("{chatId}")]
    public async Task<GetStoryResponse> GetStoryAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid chatId,
        [FromBody] GetStoryRequest? request,
        CancellationToken cancellationToken)
        => request == null
            ? await mediator.Send(new GetStoryQuery(chatId), cancellationToken)
            : await mediator.Send(new GetStoryQuery(chatId)
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            }, cancellationToken);
}