#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Grpc.Clients.FileClient;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Exceptions.SongExceptions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Music.EditSong;

#endregion

namespace RussianSpotify.API.Core.Requests.Music.PatchEditSong;

/// <summary>
///     Обработчик команды на обновление данных о песне
/// </summary>
public class PatchEditSongCommandHandler : IRequestHandler<PatchEditSongCommand, EditSongResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly IFileServiceClient _fileServiceClient;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст базы данных</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    /// <param name="fileServiceClient">Сервис для работы с файлами</param>
    public PatchEditSongCommandHandler(IDbContext dbContext, IUserContext userContext,
        IFileServiceClient fileServiceClient)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _fileServiceClient = fileServiceClient;
    }

    /// <inheritdoc />
    public async Task<EditSongResponse> Handle(PatchEditSongCommand request, CancellationToken cancellationToken)
    {
        // Достаем песню из бд
        var songFromDb = await _dbContext.Songs
                             .Include(i => i.Authors)
                             .FirstOrDefaultAsync(i => i.Id == request.SongId, cancellationToken)
                         ?? throw new EntityNotFoundException<Song>(request.SongId);

        var songOldName = songFromDb.SongName;

        // Проверка, является ли текущий пользователь автором данной песни
        var currentUserId = _userContext.CurrentUserId;
        if (currentUserId is null)
            throw new ForbiddenException();

        if (songFromDb.Authors.All(i => i.Id != currentUserId))
            throw new SongForbiddenException("User is not Author of this Song");

        // Проверка, было ли введено новое название песни и обновляем если да
        if (!string.IsNullOrEmpty(request.SongName) &&
            !string.IsNullOrWhiteSpace(request.SongName))
            songFromDb.SongName = request.SongName;

        // Проверяем, была ли введена продолжительность песни
        if (request.Duration is not null)
        {
            // Валидация и добавление
            if (request.Duration < 0)
                throw new SongBadRequestException("Wrong song duration was provided");

            songFromDb.Duration = request.Duration.Value;
        }

        // Проверяем, была ли введена новая категория
        if (request.Category is not null)
        {
            // Получаем категорию из бд
            var categoryFromDb = await _dbContext.Categories
                                     .FirstOrDefaultAsync(i => (int)i.CategoryName == request.Category,
                                         cancellationToken)
                                 ?? throw new EntityNotFoundException<Category>(request.Category.ToString()!);

            songFromDb.Category = categoryFromDb;
        }

        // Проверяем, был ли введен Id картинки
        if (request.ImageId is not null)
        {
            // Достаем картину из бд
            var file = await _fileServiceClient.GetFileAsync(request.ImageId.Value, cancellationToken);

            // Проверка, является ли файл картинкой и присвоение
            if (!_fileServiceClient.IsImage(file.Metadata.ContentType))
                throw new SongBadImageException("File's content type is not Image");

            // Удаляем текущую картинку
            if (songFromDb.ImageFileId is not null)
                await _fileServiceClient.DeleteAsync(songFromDb.ImageFileId.Value, cancellationToken);

            songFromDb.ImageFileId = request.ImageId.Value;
        }

        // Проверяем, был лы введен Id файла песни
        if (request.SongFileId is not null)
        {
            // Достаем файл из бд
            var file = await _fileServiceClient.GetFileMetadataAsync(request.SongFileId.Value, cancellationToken);

            // Проверяем, является ли файл аудио и присвоение
            if (!_fileServiceClient.IsAudio(file.ContentType))
                throw new SongBadFileException("File's content type is not Audio");

            songFromDb.SongFileId = request.SongFileId.Value;
        }

        // Вносим изменения в бд
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new EditSongResponse
        {
            SongId = songFromDb.Id,
            SongName = songOldName
        };
    }
}