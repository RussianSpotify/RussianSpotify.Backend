namespace RussianSpotify.Contracts.Requests.Playlist.PutPlaylist;

/// <summary>
/// Запрос на изменение альбома/плейлиста
/// </summary>
public class PutPlaylistRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public PutPlaylistRequest()
    {
    }

    public PutPlaylistRequest(PutPlaylistRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        PlaylistName = request.PlaylistName;
        ImageId = request.ImageId;
        SongsIds = request.SongsIds;
    }

    /// <summary>
    /// Название
    /// </summary>
    public string? PlaylistName { get; }

    /// <summary>
    /// ИД фото
    /// </summary>
    public Guid? ImageId { get; }

    /// <summary>
    /// ИД песней
    /// </summary>
    public List<Guid>? SongsIds { get; }
}