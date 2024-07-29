namespace RussianSpotify.Contracts.Requests.Playlist.PostCreatePlaylist;

/// <summary>
/// Запрос на создание плейлиста и добавление музыки
/// </summary>
public class PostCreatePlaylistRequest
{
    /// <summary>
    /// Констркутор
    /// </summary>
    public PostCreatePlaylistRequest()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public PostCreatePlaylistRequest(PostCreatePlaylistRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        PlaylistName = request.PlaylistName;
        ImageId = request.ImageId;
        SongIds = request.SongIds;
        IsAlbum = request.IsAlbum;
    }

    /// <summary>
    /// Название плейлиста
    /// </summary>
    public string PlaylistName { get; } = default!;

    /// <summary>
    /// Картинка плейлиста
    /// </summary>
    public Guid? ImageId { get; }

    /// <summary>
    /// Песни
    /// </summary>
    public List<Guid> SongIds { get; } = new();

    /// <summary>
    /// Это альбом
    /// </summary>
    public bool IsAlbum { get; }
}