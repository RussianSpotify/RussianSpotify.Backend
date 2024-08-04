using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.Playlist;
using RussianSpotify.Contracts.Requests.Playlist.DeletePlaylist;

namespace RussianSpotify.API.Core.Requests.Playlist.DeletePlaylist;

/// <summary>
/// Обработчик для <see cref="DeletePlaylistCommand"/>
/// </summary>
public class DeletePlaylistCommandHandler : IRequestHandler<DeletePlaylistCommand, DeletePlaylistResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IFileHelper _fileHelper;
    private readonly IUserContext _userContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="fileHelper">Сервис файлов</param>
    /// <param name="userContext">Контекст пользователя</param>
    public DeletePlaylistCommandHandler(IDbContext dbContext, IFileHelper fileHelper, IUserContext userContext)
    {
        _dbContext = dbContext;
        _fileHelper = fileHelper;
        _userContext = userContext;
    }

    /// <inheritdoc/>
    public async Task<DeletePlaylistResponse> Handle(DeletePlaylistCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var playlistFromDb = await _dbContext.Playlists
            .Include(i => i.Image)
            .FirstOrDefaultAsync(i => i.Id == request.PlaylistId, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.Playlist>(request.PlaylistId);

        if (playlistFromDb.AuthorId != _userContext.CurrentUserId)
            throw new PlaylistForbiddenException("You're not author of this playlist");
        
        if (playlistFromDb.Image is not null)
            await _fileHelper.DeleteFileAsync(playlistFromDb.Image, cancellationToken);

        _dbContext.Playlists.Remove(playlistFromDb);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new DeletePlaylistResponse
        {
            PlaylistId = playlistFromDb.Id,
            PlaylistName = playlistFromDb.PlaylistName
        };
    }
}