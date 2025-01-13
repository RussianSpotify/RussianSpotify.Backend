#region

using MediatR;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.Contracts.Requests.Playlist.DeletePlaylist;

#endregion

namespace RussianSpotify.API.Core.Requests.Playlist.DeletePlaylist;

/// <summary>
///     Команда на удаление <see cref="Playlist" />
/// </summary>
public class DeletePlaylistCommand : IRequest<DeletePlaylistResponse>
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="playlistId">ИД плейлист</param>
    public DeletePlaylistCommand(Guid playlistId)
        => PlaylistId = playlistId;

    /// <summary>
    ///     Ид плейлиста
    /// </summary>
    public Guid PlaylistId { get; set; }
}