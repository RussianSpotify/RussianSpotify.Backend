#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Extensions;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Music.GetSongsByFilter;

#endregion

namespace RussianSpotify.API.Core.Requests.Music.GetSongByFilter;

/// <summary>
///     Обработчик для <see cref="GetSongsByFilterQuery" />
/// </summary>
public class GetSongsByFilterQueryHandler
    : IRequestHandler<GetSongsByFilterQuery, GetSongsByFilterResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IFilterHandler _filterHandler;
    private readonly IUserContext _userContext;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="filterHandler">Фильтр хэндлер</param>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст пользователя</param>
    public GetSongsByFilterQueryHandler(IFilterHandler filterHandler, IDbContext dbContext, IUserContext userContext)
    {
        _filterHandler = filterHandler;
        _dbContext = dbContext;
        _userContext = userContext;
    }

    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}" />
    public async Task<GetSongsByFilterResponse> Handle(GetSongsByFilterQuery request,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userId = _userContext.CurrentUserId;

        if (userId is null)
            throw new CurrentUserIdNotFound("UserId из Claims не был найден");

        var query = _dbContext.Songs.AsQueryable();

        var filteredSongs = await _filterHandler.GetByFilterAsync(
            query,
            request.FilterName,
            request.FilterValue,
            cancellationToken);

        var totalCount = await filteredSongs.CountAsync(cancellationToken);

        var resultSongs = await filteredSongs
            .Include(song => song.Buckets)
            .Select(song => new GetSongsByFilterResponseItem
            {
                ImageId = song.ImageFileId,
                SongId = song.Id,
                SongName = song.SongName,
                Duration = song.Duration,
                Category = song.Category.CategoryName.GetDescription(),
                Authors = song.Authors
                    .Select(y => new GetSongByFilterResponseItemAuthor
                    {
                        AuthorId = y.Id,
                        AuthorName = y.UserName
                    })
                    .ToList(),
                IsInFavorite = song.Buckets
                    .Any(bucket => bucket.UserId.Equals(userId.Value))
            })
            .SkipTake(request: request)
            .ToListAsync(cancellationToken);

        return new GetSongsByFilterResponse(resultSongs, totalCount);
    }
}