using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.Playlist;
using RussianSpotify.Contracts.Requests.Playlist.PutPlaylist;

namespace RussianSpotify.API.Core.Requests.Playlist.PutPlaylist;

/// <summary>
/// Обработчик для <see cref="PutPlaylistCommand"/>
/// </summary>
public class PutPlaylistCommandHandler : IRequestHandler<PutPlaylistCommand, PutPlaylistResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly IFileHelper _fileHelper;
    private readonly ILogger<PutPlaylistCommandHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="fileHelper"></param>
    /// <param name="logger">Логгер</param>
    public PutPlaylistCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        IFileHelper fileHelper,
        ILogger<PutPlaylistCommandHandler> logger)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _fileHelper = fileHelper;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<PutPlaylistResponse> Handle(PutPlaylistCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        try
        {
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            var playlist = await _dbContext.Playlists
               .Include(x => x.Songs)
               .Include(i => i.Image)
               .Where(x => x.AuthorId == _userContext.CurrentUserId)
               .FirstOrDefaultAsync(x => x.Id == request.PlaylistId, cancellationToken)
                ?? throw new EntityNotFoundException<Entities.Playlist>(request.PlaylistId);

            playlist.PlaylistName = request.PlaylistName ?? playlist.PlaylistName;

            var songsToDelete = playlist.Songs!
                .Where(x => request.SongsIds!.All(y => y != x.Id))
                .Select(x => x.Id)
                .ToList();
        
            songsToDelete.ForEach(x =>
            {
                if (playlist.Songs?.Any(y => y.Id == x) == true)
                {
                    var song = playlist.Songs.FirstOrDefault(z => z.Id == x)
                               ?? throw new EntityNotFoundException<Song>(x);
                    
                    playlist.Songs.Remove(song);
                }
            });
            
            if (request.SongsIds is not null)
                foreach (var songId in request.SongsIds)
                {
                    if (playlist.Songs?.Select(x => x.Id).Contains(songId) == true)
                        continue;

                    var newSong = await _dbContext.Songs
                        .FirstOrDefaultAsync(x => x.Id == songId, cancellationToken)
                        ?? throw new EntityNotFoundException<Song>(songId);

                    playlist.Songs?.Add(newSong);
                }

            if (request.ImageId is not null)
            {
                // Достаем картину из бд
                var imageFromDb = await _dbContext.Files
                    .FirstOrDefaultAsync(i => i.Id == request.ImageId.Value, cancellationToken);

                if (imageFromDb is null)
                    throw new PlaylistFileException("File not found");
                
                // Проверка, является ли файл картинкой и присвоение
                if (!_fileHelper.IsImage(imageFromDb))
                    throw new PlaylistBadImageException("File's content type is not Image");

                // Удаляем текущую картинку
                if (playlist.Image is not null)
                    await _fileHelper.DeleteFileAsync(playlist.Image, cancellationToken);

                playlist.Image = imageFromDb;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            
            return new PutPlaylistResponse
            {
                PlaylistName = playlist.PlaylistName,
                PlaylistId = playlist.Id
            };
        }
        catch (Exception e)
        {
            await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
            _logger.LogCritical(e.Message);
            throw;
        }
    }
}