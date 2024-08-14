using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.SongExceptions;
using RussianSpotify.Contracts.Requests.Music.DeleteSong;

namespace RussianSpotify.API.Core.Requests.Music.DeleteSong;

/// <summary>
/// Обработчик для <see cref="DeleteSongCommand"/>
/// </summary>
public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand, DeleteSongResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly IFileHelper _fileHelper;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст базы данных</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    /// <param name="fileHelper">Сервис-помощник для работы с файлами</param>
    public DeleteSongCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        IFileHelper fileHelper)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _fileHelper = fileHelper;
    }

    /// <inheritdoc/> 
    public async Task<DeleteSongResponse> Handle(DeleteSongCommand request, CancellationToken cancellationToken)
    {
        // Достаем песню из бд
        var song = await _dbContext.Songs
            .Include(i => i.Authors)
            .FirstOrDefaultAsync(i => i.Id == request.SongId, cancellationToken)
            ?? throw new EntityNotFoundException<Song>(request.SongId);

        // Проверка, является ли текущий пользователь автором данной песни
        var currentUserId = _userContext.CurrentUserId;
        if (currentUserId is null)
            throw new ForbiddenException();

        if (song.Authors.All(i => i.Id != currentUserId))
            throw new ForbiddenException();

        var result = new DeleteSongResponse
        {
            SongId = song.Id,
            SongName = song.SongName
        };

        foreach (var file in song.Files)
            await _fileHelper.DeleteFileAsync(file, cancellationToken);
        
        // Вносим изменения в бд
        _dbContext.Songs.Remove(song);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return result;
    }
}