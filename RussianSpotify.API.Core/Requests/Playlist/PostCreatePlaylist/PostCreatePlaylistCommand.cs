#region

using MediatR;
using RussianSpotify.Contracts.Requests.Playlist.PostCreatePlaylist;

#endregion

namespace RussianSpotify.API.Core.Requests.Playlist.PostCreatePlaylist;

/// <summary>
///     Команда на создание плейлиста/альбома
/// </summary>
public class PostCreatePlaylistCommand : PostCreatePlaylistRequest, IRequest<PostCreatePlaylistResponse>
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostCreatePlaylistCommand(PostCreatePlaylistRequest request)
        : base(request)
    {
    }
}