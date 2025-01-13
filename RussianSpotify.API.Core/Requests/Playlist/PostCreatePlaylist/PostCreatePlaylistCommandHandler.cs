#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.Playlist;
using RussianSpotify.API.Grpc.Clients.FileClient;
using RussianSpotify.API.Shared.Domain.Constants;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Playlist.PostCreatePlaylist;

#endregion

namespace RussianSpotify.API.Core.Requests.Playlist.PostCreatePlaylist;

/// <summary>
///     Обработчия для <see cref="PostCreatePlaylistCommand" />
/// </summary>
public class PostCreatePlaylistCommandHandler : IRequestHandler<PostCreatePlaylistCommand, PostCreatePlaylistResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserContext _userContext;
    private readonly IFileServiceClient _fileServiceClient;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="dateTimeProvider">Провайдер даты</param>
    /// <param name="fileServiceClient">Сервис для работы с файлами</param>
    public PostCreatePlaylistCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        IDateTimeProvider dateTimeProvider,
        IFileServiceClient fileServiceClient)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _dateTimeProvider = dateTimeProvider;
        _fileServiceClient = fileServiceClient;
    }

    /// <inheritdoc />
    public async Task<PostCreatePlaylistResponse> Handle(PostCreatePlaylistCommand request,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var currentUser = await _dbContext.Users
                              .Include(x => x.AuthorPlaylists)
                              .FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId, cancellationToken)
                          ?? throw new EntityNotFoundException<User>(_userContext.CurrentUserId!.Value);

        var userRoles = new List<string>();

        var isArtist = userRoles.Contains(Roles.AdminRoleName) || userRoles.Contains(Roles.AuthorRoleName);

        if (!isArtist && request.IsAlbum)
            throw new ApplicationBaseException("Пользователь не может создать альбом, только плейлист");

        var songs = await _dbContext.Songs
            .Where(x => request.SongIds.Contains(x.Id))
            .ToListAsync(cancellationToken);

        var playlist = new Entities.Playlist
        {
            PlaylistName = request.PlaylistName,
            ImageFileId = null,
            IsAlbum = request.IsAlbum,
            Songs = songs,
            Author = currentUser,
            ReleaseDate = _dateTimeProvider.CurrentDate,
            Users = new List<User>
            {
                currentUser
            }
        };

        if (request.ImageId.HasValue)
        {
            var fileMetadata = await _fileServiceClient.GetFileMetadataAsync(request.ImageId.Value, cancellationToken);

            if (!_fileServiceClient.IsImage(fileMetadata.ContentType))
                throw new PlaylistFileException("File is not Image");

            playlist.ImageFileId = request.ImageId.Value;
        }

        currentUser.AuthorPlaylists.Add(playlist);

        await _dbContext.Playlists.AddAsync(playlist, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PostCreatePlaylistResponse
        {
            PlaylistName = playlist.PlaylistName,
            PlaylistId = playlist.Id
        };
    }
}