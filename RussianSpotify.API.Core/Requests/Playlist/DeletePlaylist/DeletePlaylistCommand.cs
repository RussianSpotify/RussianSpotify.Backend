using MediatR;
using RussianSpotify.Contracts.Requests.Playlist.DeletePlaylist;

namespace RussianSpotify.API.Core.Requests.Playlist.DeletePlaylist;

using Entities;

/// <summary>
/// Команда на удаление <see cref="Playlist"/>
/// </summary>
public class DeletePlaylistCommand : IRequest<DeletePlaylistResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="playlistId">ИД плейлист</param>
    public DeletePlaylistCommand(Guid playlistId)
        => PlaylistId = playlistId;

    /// <summary>
    /// Ид плейлиста
    /// </summary>
    public Guid PlaylistId { get; set; }
}