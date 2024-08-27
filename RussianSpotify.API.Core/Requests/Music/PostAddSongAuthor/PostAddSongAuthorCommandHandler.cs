using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.DefaultSettings;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.SongExceptions;
using RussianSpotify.Contracts.Requests.Music.AddSongAuthor;

namespace RussianSpotify.API.Core.Requests.Music.PostAddSongAuthor;

/// <summary>
/// Обработчик запроса на добавление автора песни
/// </summary>
public class PostAddSongAuthorCommandHandler : IRequestHandler<PostAddSongAuthorCommand, AddSongAuthorResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly IRoleManager _roleManager;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст базы данных</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    /// <param name="roleManager">Взаимодействует с ролью пользователя</param>
    public PostAddSongAuthorCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        IRoleManager roleManager)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _roleManager = roleManager;
    }

    /// <inheritdoc/>
    public async Task<AddSongAuthorResponse> Handle(PostAddSongAuthorCommand request,
        CancellationToken cancellationToken)
    {
        // Достаем песню из бд
        var songFromDb = await _dbContext.Songs
            .Include(i => i.Authors)
            .FirstOrDefaultAsync(i => i.Id == request.SongId, cancellationToken)
            ?? throw new EntityNotFoundException<Song>(request.SongId);

        if (songFromDb.Authors.All(i => i.Id != _userContext.CurrentUserId))
            throw new ForbiddenException();

        if (songFromDb.Authors.Any(x => x.Email == request.AuthorEmail))
            throw new SongBadRequestException("User is already author of this Song");
        
        // Достаем нового автора из бд
        var userFromDb = await _dbContext.Users
            .FirstOrDefaultAsync(i => i.Email!.Equals(request.AuthorEmail), cancellationToken)
            ?? throw new EntityNotFoundException<User>(request.AuthorEmail);

        // Проверка, является ли добавляемый пользовател автором
        var ifContainsAuthorRole = _roleManager.IsInRole(
            userFromDb,
            BaseRoles.AuthorRoleName);
        
        if (!ifContainsAuthorRole)
            throw new SongBadRequestException("User is not Author");
        
        // Вносим изменения в бд
        songFromDb.AddAuthor(userFromDb);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddSongAuthorResponse
        {
            SongId = songFromDb.Id,
            AuthorId = userFromDb.Id,
            AuthorName = userFromDb.UserName!
        };
    }
}