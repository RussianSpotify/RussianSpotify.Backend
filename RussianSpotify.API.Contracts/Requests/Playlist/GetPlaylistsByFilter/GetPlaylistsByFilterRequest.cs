﻿#region

using RussianSpotify.Contracts.Models;

#endregion

namespace RussianSpotify.Contracts.Requests.Playlist.GetPlaylistsByFilter;

public class GetPlaylistsByFilterRequest : IPaginationFilter
{
    private readonly int _pageSize;
    private readonly int _pageNumber;

    /// <summary>
    ///     Конструктор
    /// </summary>
    public GetPlaylistsByFilterRequest()
    {
        _pageNumber = DefaultsPagination.PageNumber;
        _pageSize = DefaultsPagination.PageSize;
    }

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    protected GetPlaylistsByFilterRequest(GetPlaylistsByFilterRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        PageNumber = request.PageNumber;
        PageSize = request.PageSize;
        FilterName = request.FilterName;
        FilterValue = request.FilterValue;
    }

    /// <summary>
    ///     Название фильтра(Доступные фильтры: AuthorPlaylists)
    /// </summary>
    public string FilterName { get; set; }

    /// <summary>
    ///     Значение фильтра
    /// </summary>
    public string FilterValue { get; set; }

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
        init => _pageSize = value > 0 ? value : DefaultsPagination.PageSize;
    }
}