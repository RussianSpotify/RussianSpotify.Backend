namespace RussianSpotify.Contracts.Requests.Music.GetSongsByFilter;

/// <summary>
/// Отфильтрованные песни
/// </summary>
public class GetSongsByFilterResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="entities">Сущности</param>
    /// <param name="totalCount">Общее кол-во</param>
    public GetSongsByFilterResponse(List<GetSongsByFilterResponseItem> entities, int totalCount)
    {
        Entities = entities;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Отфильтрованные песни
    /// </summary>
    public List<GetSongsByFilterResponseItem> Entities { get; }

    /// <summary>
    /// Общее кол-во подобных треков
    /// </summary>
    public int TotalCount { get; set; }
}