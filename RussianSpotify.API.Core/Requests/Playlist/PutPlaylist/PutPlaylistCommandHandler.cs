#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.Playlist;
using RussianSpotify.API.Grpc.Clients.FileClient;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Playlist.PutPlaylist;

#endregion

namespace RussianSpotify.API.Core.Requests.Playlist.PutPlaylist;

/// <summary>
///     Обработчик для <see cref="PutPlaylistCommand" />
/// </summary>
public class PutPlaylistCommandHandler : IRequestHandler<PutPlaylistCommand, PutPlaylistResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly IFileServiceClient _fileServiceClient;
    private readonly ILogger<PutPlaylistCommandHandler> _logger;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="logger">Логгер</param>
    /// <param name="fileServiceClient">Сервис для работы с файлами</param>
    public PutPlaylistCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        ILogger<PutPlaylistCommandHandler> logger,
        IFileServiceClient fileServiceClient)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _logger = logger;
        _fileServiceClient = fileServiceClient;
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
                var image =
                    await _fileServiceClient.GetFileMetadataAsync(request.ImageId, cancellationToken);

                if (image is null)
                    throw new PlaylistFileException("File not found");

                // Проверка, является ли файл картинкой и присвоение
                if (!_fileServiceClient.IsImage(image.ContentType))
                    throw new PlaylistBadImageException("File's content type is not Image");

                // Удаляем текущую картинку
                if (playlist.ImageFileId is not null)
                    await _fileServiceClient.DeleteAsync(playlist.ImageFileId, cancellationToken);

                playlist.ImageFileId = request.ImageId;
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