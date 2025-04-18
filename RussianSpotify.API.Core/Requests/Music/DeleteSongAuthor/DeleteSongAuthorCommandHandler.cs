#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Exceptions.SongExceptions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Music.DeleteSongAuthor;

#endregion

namespace RussianSpotify.API.Core.Requests.Music.DeleteSongAuthor;

/// <summary>
///     Обработчик запроса на удаление автора песни
/// </summary>
public class DeleteSongAuthorCommandHandler : IRequestHandler<DeleteSongAuthorCommand, DeleteSongAuthorResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст базы данных</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public DeleteSongAuthorCommandHandler(IDbContext dbContext, IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }

    /// <inheritdoc />
    public async Task<DeleteSongAuthorResponse> Handle(DeleteSongAuthorCommand request,
        CancellationToken cancellationToken)
    {
        // Достаем песню из бд
        var song = await _dbContext.Songs
                       .Include(i => i.Authors)
                       .FirstOrDefaultAsync(i => i.Id == request.SongId, cancellationToken)
                   ?? throw new EntityNotFoundException<Song>(request.SongId);

        // Достаем текущего пользователя из бд
        var currentUserId = _userContext.CurrentUserId;
        if (currentUserId is null)
            throw new ForbiddenException();

        // Проверка на то, пользователь не пытается удалить самого себя
        if (currentUserId == request.AuthorId)
            throw new SongForbiddenException("Author can't remove themself");

        // Проверка, является ли текущий пользователь автором данной песни
        if (song.Authors.All(i => i.Id != currentUserId))
            throw new ForbiddenException();

        // Достаем автора, которого хотим удалить
        var songAuthorToDelete = song.Authors
                                     .FirstOrDefault(i => i.Id == request.AuthorId)
                                 ?? throw new EntityNotFoundException<User>(request.AuthorId);

        // Вносим изменения в бд
        song.RemoveAuthor(songAuthorToDelete);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteSongAuthorResponse
        {
            SongId = song.Id,
            AuthorId = songAuthorToDelete.Id
        };
    }
}