#region

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RussianSpotify.API.Core.Requests.Playlist.DeletePlaylist;
using RussianSpotify.API.Core.Requests.Playlist.GetPlaylistById;
using RussianSpotify.API.Core.Requests.Playlist.GetPlaylistsByFilter;
using RussianSpotify.API.Core.Requests.Playlist.PostAddPlaylistToFavourite;
using RussianSpotify.API.Core.Requests.Playlist.PostCreatePlaylist;
using RussianSpotify.API.Core.Requests.Playlist.PutPlaylist;
using RussianSpotify.API.Core.Requests.Playlist.RemovePlaylistFromFavorite;
using RussianSpotify.Contracts.Requests.Playlist.DeletePlaylist;
using RussianSpotify.Contracts.Requests.Playlist.GetFavouritePlaylistById;
using RussianSpotify.Contracts.Requests.Playlist.GetPlaylistsByFilter;
using RussianSpotify.Contracts.Requests.Playlist.PostCreatePlaylist;
using RussianSpotify.Contracts.Requests.Playlist.PutPlaylist;

#endregion

namespace RussianSpotify.API.WEB.Controllers;

/// <summary>
///     Контроллер, отвечающий за работу с плейлистами
/// </summary>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class PlaylistController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="mediator">Медиатор</param>
    public PlaylistController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Получить альбомы по фильтру(Доступные фильтры: AuthorPlaylists, PlaylistName, FavoritePlaylist)
    /// </summary>
    /// <param name="request">
    ///     GetPlaylistsByFilterRequest(Название фильтра,
    ///     значение фильтра, страница, кол-во альбомов на странице)
    /// </param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список GetPlaylistsByFilterResponse альбомы по фильтру</returns>
    [HttpGet("GetPlaylistsByFilter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<GetPlaylistsByFilterResponse> GetPlaylistsByFilter(
        [FromQuery] GetPlaylistsByFilterRequest request,
        CancellationToken cancellationToken)
    {
        var query = new GetPlaylistsByFilterQuery(request);
        return await _mediator.Send(query, cancellationToken);
    }

    /// <summary>
    ///     Создать плейлист/альбом
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("CreatePlaylist")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    [ProducesResponseType(500)]
    public async Task<PostCreatePlaylistResponse> PostCreatePlaylistAsync(
        [FromBody] PostCreatePlaylistRequest request,
        CancellationToken cancellationToken)
        => await _mediator.Send(new PostCreatePlaylistCommand(request), cancellationToken);

    /// <summary>
    ///     Добавить альбом/плейлист в любимое
    /// </summary>
    /// <param name="id">ИД альбома/плейлиста</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("Playlist/{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    [ProducesResponseType(500)]
    public async Task PostAddPlaylistToFavouriteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await _mediator.Send(new PostAddPlaylistToFavouriteCommand(id), cancellationToken);

    /// <summary>
    ///     Изменить плейлист/альбом
    /// </summary>
    /// <param name="playlistId">ИД плейлиста</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPut("EditPlaylist/{playlistId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    [ProducesResponseType(500)]
    public async Task<PutPlaylistResponse> PutPlaylistAsync(
        [FromRoute] Guid playlistId,
        [FromBody] PutPlaylistRequest request, CancellationToken cancellationToken)
        => await _mediator.Send(new PutPlaylistCommand(
                request: request,
                playlistId: playlistId),
            cancellationToken);

    /// <summary>
    ///     Получить инфу о плейлисте/альбоме
    /// </summary>
    /// <param name="playlistId">ИД плейлиста/альбома</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Плейлист/альбом</returns>
    [HttpGet("GetPlaylist/{playlistId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<GetFavouritePlaylistByIdResponse> GetPlaylistByIdAsync(
        [FromRoute] Guid playlistId,
        CancellationToken cancellationToken)
        => await _mediator.Send(new GetPlaylistByIdQuery(playlistId: playlistId), cancellationToken);

    /// <summary>
    ///     Удалить плейлист
    /// </summary>
    /// <param name="playlistId">Id плейлиста</param>
    [HttpDelete("DeletePlaylist/{playlistId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<DeletePlaylistResponse> DeletePlaylistAsync([FromRoute] Guid playlistId)
        => await _mediator.Send(new DeletePlaylistCommand(playlistId));

    /// <summary>
    ///     Удалить плейлист из любимых
    /// </summary>
    /// <param name="playlistId">ИД плейлиста</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("RemovePlaylistFromFavorite/{playlistId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(403)]
    [ProducesResponseType(500)]
    public async Task RemovePlaylistFromFavoriteAsync([FromRoute] Guid playlistId, CancellationToken cancellationToken)
        => await _mediator.Send(new RemovePlaylistFromFavoriteCommand(playlistId), cancellationToken);
}