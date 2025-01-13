#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.Playlist;
using RussianSpotify.API.Grpc.Clients.FileClient;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Playlist.DeletePlaylist;

#endregion

namespace RussianSpotify.API.Core.Requests.Playlist.DeletePlaylist;

/// <summary>
///     Обработчик для <see cref="DeletePlaylistCommand" />
/// </summary>
public class DeletePlaylistCommandHandler : IRequestHandler<DeletePlaylistCommand, DeletePlaylistResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly IFileServiceClient _fileServiceClient;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="fileServiceClient">Сервис для работы с файлами</param>
    public DeletePlaylistCommandHandler(IDbContext dbContext, IUserContext userContext,
        IFileServiceClient fileServiceClient)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _fileServiceClient = fileServiceClient;
    }

    /// <inheritdoc />
    public async Task<DeletePlaylistResponse> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var playlistFromDb = await _dbContext.Playlists
                                 .FirstOrDefaultAsync(i => i.Id == request.PlaylistId, cancellationToken)
                             ?? throw new EntityNotFoundException<Entities.Playlist>(request.PlaylistId);

        if (playlistFromDb.AuthorId != _userContext.CurrentUserId)
            throw new PlaylistForbiddenException("You're not author of this playlist");

        if (playlistFromDb.ImageFileId is not null)
            await _fileServiceClient.DeleteAsync(playlistFromDb.ImageFileId, cancellationToken);

        _dbContext.Playlists.Remove(playlistFromDb);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new DeletePlaylistResponse
        {
            PlaylistId = playlistFromDb.Id,
            PlaylistName = playlistFromDb.PlaylistName
        };
    }
}