using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.SongExceptions;
using RussianSpotify.Contracts.Requests.Music.AddSong;

namespace RussianSpotify.API.Core.Requests.Music.PostAddSong;

/// <summary>
/// Обработчик команды на добавление новой песни
/// </summary>
public class PostAddSongCommandHandler : IRequestHandler<PostAddSongCommand, AddSongResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly IFileHelper _fileHelper;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Конекст базы данных</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    /// <param name="fileHelper">Сервис для работы с файлами</param>
    public PostAddSongCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        IFileHelper fileHelper)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _fileHelper = fileHelper;
    }

    /// <inheritdoc/>
    public async Task<AddSongResponse> Handle(PostAddSongCommand request, CancellationToken cancellationToken)
    {
        // Достаем категорию из бд
        var category = await _dbContext.Categories
            .FirstOrDefaultAsync(i => (int)i.CategoryName == request.Category, cancellationToken)
            ?? throw new EntityNotFoundException<Category>(request.Category.ToString());

        // Валидация названия песни
        if (string.IsNullOrEmpty(request.SongName) || string.IsNullOrWhiteSpace(request.SongName))
            throw new SongBadRequestException("Wrong song name was provided");

        // Валидация длительности песни
        if (request.Duration < 0)
            throw new SongBadRequestException("Wrong song duration was provided");

        var newSong = new Song(
            request.SongName,
            request.Duration,
            category);

        // Если был введен Id картинки, добавляем его в песню
        if (request.ImageId is not null)
        {
            var imageFromDb = await _dbContext.Files
                .FirstOrDefaultAsync(i => i.Id == request.ImageId.Value,
                    cancellationToken: cancellationToken);

            if (imageFromDb is null)
                throw new SongBadRequestException("Image not found");

            // Проверка, является ли файл картинкой
            if (!_fileHelper.IsImage(imageFromDb))
                throw new SongBadImageException("File's content type is not Image");

            newSong.Image = imageFromDb;
            newSong.Files.Add(imageFromDb);
        }

        // Если был введен Id файла песни, то связываем файл с песней
        if (request.SongFileId is not null)
        {
            var fileFromDb = await _dbContext.Files
                .FirstOrDefaultAsync(i => i.Id == request.SongFileId.Value, cancellationToken);

            if (fileFromDb is null)
                throw new SongBadRequestException("Song File not found");

            // Проверка, является ли файл аудио
            if (!_fileHelper.IsAudio(fileFromDb))
                throw new SongBadFileException("File's content type is not Audio");

            newSong.Files.Add(fileFromDb);
        }
        
        var userFromDb = await _dbContext.Users
            .FirstOrDefaultAsync(i => i.Id == _userContext.CurrentUserId, cancellationToken)
            ?? throw new ForbiddenException();

        // Вносим изменения в бд
        newSong.AddAuthor(userFromDb);
        await _dbContext.Songs.AddAsync(newSong, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddSongResponse
        {
            SongId = newSong.Id,
            SongName = newSong.SongName
        };
    }
}