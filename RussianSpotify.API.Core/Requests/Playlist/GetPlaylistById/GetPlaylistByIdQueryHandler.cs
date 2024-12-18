using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Playlist.GetFavouritePlaylistById;

namespace RussianSpotify.API.Core.Requests.Playlist.GetPlaylistById;

/// <summary>
/// Обработчик для <see cref="GetPlaylistByIdQuery"/>
/// </summary>
public class GetPlaylistByIdQueryHandler
    : IRequestHandler<GetPlaylistByIdQuery, GetFavouritePlaylistByIdResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст юзера</param>
    public GetPlaylistByIdQueryHandler(IDbContext dbContext, IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }

    /// <inheritdoc />
    public async Task<GetFavouritePlaylistByIdResponse> Handle(
        GetPlaylistByIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        return await _dbContext.Playlists
           .Select(x => new GetFavouritePlaylistByIdResponse
           {
               Id = x.Id,
               PlaylistName = x.PlaylistName,
               ImageId = x.ImageFileId,
               IsAlbum = x.IsAlbum,
               AuthorId = x.AuthorId,
               AuthorName = x.Author!.UserName,
               ReleaseDate = x.ReleaseDate,
               IsInFavorite = x.Users!.Any(y => y.Id == _userContext.CurrentUserId)
           })
           .FirstOrDefaultAsync(x => x.Id == request.PlaylistId, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.Playlist>(request.PlaylistId);
    }
}