#region

using MediatR;
using RussianSpotify.Contracts.Requests.Playlist.PutPlaylist;

#endregion

namespace RussianSpotify.API.Core.Requests.Playlist.PutPlaylist;

/// <summary>
///     Команда на редактирование плейлиста/альбома
/// </summary>
public class PutPlaylistCommand : PutPlaylistRequest, IRequest<PutPlaylistResponse>
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="playlistId">ИД плейлиста/альбома</param>
    public PutPlaylistCommand(PutPlaylistRequest request, Guid playlistId)
        : base(request)
    {
        PlaylistId = playlistId;
    }

    /// <summary>
    ///     ИД альбома/плейлиста
    /// </summary>
    public Guid PlaylistId { get; set; }
}