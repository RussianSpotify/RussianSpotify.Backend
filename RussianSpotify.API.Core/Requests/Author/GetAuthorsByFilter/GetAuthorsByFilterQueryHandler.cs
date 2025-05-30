#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Domain.Constants;
using RussianSpotify.Contracts.Requests.Author.GetAuthorsByFilter;

#endregion

namespace RussianSpotify.API.Core.Requests.Author.GetAuthorsByFilter;

/// <summary>
///     Обработчик запроса <see cref="GetAuthorsByFilterRequest" />
/// </summary>
public class GetAuthorsByFilterQueryHandler : IRequestHandler<GetAuthorsByFilterQuery, GetAuthorsByFilterResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IFilterHandler _filterHandler;
    private readonly IRoleManager _roleManager;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст базы данных</param>
    /// <param name="roleManager">Сервис для работы с ролями пользователей</param>
    /// <param name="filterHandler">Сервис для фильтра сущностей</param>
    public GetAuthorsByFilterQueryHandler(
        IDbContext dbContext,
        IRoleManager roleManager,
        IFilterHandler filterHandler)
    {
        _dbContext = dbContext;
        _roleManager = roleManager;
        _filterHandler = filterHandler;
    }

    /// <inheritdoc />
    public async Task<GetAuthorsByFilterResponse> Handle(
        GetAuthorsByFilterQuery request,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var query = _dbContext.Users.AsQueryable();

        var filteredUsers = await _filterHandler.GetByFilterAsync(
            query,
            request.FilterName,
            request.FilterValue,
            cancellationToken);

        var filteredUsersToList = await filteredUsers
            .Include(x => x.AuthorPlaylists)
            .Include(x => x.Roles)
            .ToListAsync(cancellationToken);

        var authors = new List<User>();

        foreach (var user in filteredUsersToList)
            if (_roleManager.IsInRole(user, Roles.AuthorRoleName))
                authors.Add(user);

        authors = authors
            .Distinct()
            .ToList();

        var totalCount = authors.Count;
        var resultAuthors = authors
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(i => new GetAuthorsByFilterResponseItem
            {
                AuthorId = i.Id,
                AuthorName = i.UserName,
                ImageId = i.UserPhotoId ?? new Guid(),
                Albums = i.AuthorPlaylists
                    .Take(request.PlaylistCount)
                    .Select(e => new GetAuthorsByFilterResponseItemPlaylist
                    {
                        PlaylistId = e.Id,
                        PlaylistName = e.PlaylistName
                    }).ToList(),
                TotalAlbumCount = i.AuthorPlaylists.Count
            })
            .ToList();

        return new GetAuthorsByFilterResponse(resultAuthors, totalCount);
    }
}