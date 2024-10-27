using RussianSpotify.Contracts.Models;

namespace RussianSpotify.Contracts.Requests.Chat.GetStory;

/// <summary>
/// Запрос на получение истории чата
/// </summary>
public class GetStoryRequest
{
    private int _pageNumber;
    private int _pageSize;

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetStoryRequest()
    {
        _pageNumber = DefaultsPagination.PageNumber;
        _pageSize = DefaultsPagination.ChatPageSize;
    }

    /// <summary>
    /// Номер страницы
    /// </summary>
    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value > 0
            ? value
            : DefaultsPagination.PageNumber;
    }

    /// <summary>
    /// Размерность
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 0
            ? value
            : DefaultsPagination.ChatPageSize;
    }
}