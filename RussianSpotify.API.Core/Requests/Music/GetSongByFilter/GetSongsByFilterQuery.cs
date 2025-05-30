﻿#region

using MediatR;
using RussianSpotify.Contracts.Models;
using RussianSpotify.Contracts.Requests.Music.GetSongsByFilter;

#endregion

namespace RussianSpotify.API.Core.Requests.Music.GetSongByFilter;

/// <summary>
///     Запрос на получение песен по фильтру
/// </summary>
public class GetSongsByFilterQuery
    : GetSongsByFilterRequest, IRequest<GetSongsByFilterResponse>, IPaginationFilter
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">GetSongsByFilterRequest реквест, который приходит с фронта</param>
    public GetSongsByFilterQuery(GetSongsByFilterRequest request)
        : base(request)
    {
    }
}