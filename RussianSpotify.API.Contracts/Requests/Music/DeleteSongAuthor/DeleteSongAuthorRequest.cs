namespace RussianSpotify.Contracts.Requests.Music.DeleteSongAuthor;

/// <summary>
/// Запрос на удаление автора песни
/// </summary>
public class DeleteSongAuthorRequest
{
    /// <summary>
    /// Пустой конструктор
    /// </summary>
    public DeleteSongAuthorRequest()
    {
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"></param>
    public DeleteSongAuthorRequest(DeleteSongAuthorRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        SongId = request.SongId;
        AuthorId = request.AuthorId;
    }

    /// <summary>
    /// Id песни
    /// </summary>
    public Guid SongId { get; set; }

    /// <summary>
    /// Id автора
    /// </summary>
    public Guid AuthorId { get; set; }
}