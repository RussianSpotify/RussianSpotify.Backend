#region

using RussianSpotify.Contracts.Models;

#endregion

namespace RussianSpotify.Contracts.Requests.Author.GetAuthorsByFilter;

/// <summary>
///     Запрос для получения авторов по фильтру
/// </summary>
public class GetAuthorsByFilterRequest
{
    private readonly int _pageNumber;
    private int _pageSize;

    /// <summary>
    ///     Пустой конструктор
    /// </summary>
    public GetAuthorsByFilterRequest()
    {
        _pageNumber = DefaultsPagination.PageNumber;
        _pageSize = DefaultsPagination.PageSize;
    }

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public GetAuthorsByFilterRequest(GetAuthorsByFilterRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        FilterName = request.FilterName;
        FilterValue = request.FilterValue;
        PlaylistCount = request.PlaylistCount;
        PageNumber = request.PageNumber;
        PageSize = request.PageSize;
    }

    /// <summary>
    ///     Название фильтра(Доступные фильтры: AuthorPlaylists)
    /// </summary>
    public string FilterName { get; set; } = null!;

    /// <summary>
    ///     Значение фильтра
    /// </summary>
    public string FilterValue { get; set; } = null!;

    /// <summary>
    ///     Количество плейлистов в ответе
    /// </summary>
    public int PlaylistCount { get; set; }

    /// <summary>
    ///     Номер страницы
    /// </summary>
    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = value > 0 ? value : DefaultsPagination.PageNumber;
    }

    /// <summary>
    ///     Кол-во элементов на странице
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > 0 ? value : DefaultsPagination.PageSize;
    }
}