namespace RussianSpotify.Contracts.Requests.Chat.GetChats;

/// <summary>
///     Ответ на запрос о получении чатов
/// </summary>
public class GetChatsResponse
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="entities">Сущности</param>
    /// <param name="totalCount">Общее кол-во</param>
    public GetChatsResponse(List<GetChatsResponseItem> entities, int totalCount)
    {
        Entities = entities;
        TotalCount = totalCount;
    }

    /// <summary>
    ///     Сущности
    /// </summary>
    public List<GetChatsResponseItem> Entities { get; set; }

    /// <summary>
    ///     Общее кол-во
    /// </summary>
    public int TotalCount { get; set; }
}