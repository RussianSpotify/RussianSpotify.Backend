namespace RussianSpotify.Contracts.Requests.Chat.GetStory;

/// <summary>
/// Ответ на запрос о получении истории чата
/// </summary>
public class GetStoryResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="entities">Сущности</param>
    /// <param name="totalCount">Общее кол-во</param>
    public GetStoryResponse(List<GetStoryResponseItem> entities, int totalCount)
    {
        Entities = entities;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Сущности
    /// </summary>
    public List<GetStoryResponseItem> Entities { get; set; }

    /// <summary>
    /// Общее кол-во
    /// </summary>
    public int TotalCount { get; set; }
}