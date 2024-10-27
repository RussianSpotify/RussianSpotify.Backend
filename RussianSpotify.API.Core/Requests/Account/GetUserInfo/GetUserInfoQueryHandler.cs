using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.DefaultSettings;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.AccountExceptions;
using RussianSpotify.API.Core.Exceptions.AuthExceptions;
using RussianSpotify.Contracts.Requests.Account.GetUserInfo;

namespace RussianSpotify.API.Core.Requests.Account.GetUserInfo;

/// <summary>
/// Обработчик для <see cref="GetUserInfoQuery"/>
/// </summary>
public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoResponse>
{
    private readonly IUserContext _userContext;
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userContext">Контекс текущего пользоавтеля</param>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    public GetUserInfoQueryHandler(
        IUserContext userContext,
        IDbContext dbContext)
    {
        _userContext = userContext;
        _dbContext = dbContext;
    }

    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
    public async Task<GetUserInfoResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        var user = await _dbContext.Users
            .Include(x => x.Roles)
            .Include(x => x.Chats)
            .FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId, cancellationToken)
            ?? throw new ForbiddenException();
        
        return new GetUserInfoResponse
        {
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            UserPhotoId = user.UserPhotoId,
            ChatId = user.Roles.Any(x => x.Name == BaseRoles.AdminRoleName)
                ? null
                : user.Chats.FirstOrDefault()?.Id,
            Roles = user.Roles
                .Select(x => x.Name)
                .ToList(),

        };
    }
}