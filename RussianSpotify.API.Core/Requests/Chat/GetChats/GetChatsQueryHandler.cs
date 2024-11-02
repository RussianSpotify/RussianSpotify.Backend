using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.DefaultSettings;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.Contracts.Requests.Chat.GetChats;

namespace RussianSpotify.API.Core.Requests.Chat.GetChats;

/// <summary>
/// Обработчик для <see cref="GetChatsQuery"/>
/// </summary>
public class GetChatsQueryHandler : IRequestHandler<GetChatsQuery, GetChatsResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="dbContext">Контекст БД</param>
    public GetChatsQueryHandler(IUserContext userContext, IDbContext dbContext)
    {
        _userContext = userContext;
        _dbContext = dbContext;
    }

    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
    public async Task<GetChatsResponse> Handle(GetChatsQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var currentUser = await _dbContext.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId, cancellationToken)
            ?? throw new ForbiddenException();

        var isAdmin = currentUser.Roles
            .Any(x => x.Name == BaseRoles.AdminRoleName);

        var query = _dbContext.Chats
            .Where(x => x.Users.Any(y => y.Id == _userContext.CurrentUserId));

        var totalCount = await query.CountAsync(cancellationToken);

        var chats = await query
            .Select(x => new GetChatsResponseItem
            {
                Id = x.Id,
                ChatName = isAdmin
                    ? x.Users.FirstOrDefault(y => y.Roles
                        .Select(z => z.Name)
                        .Any(z => z != BaseRoles.AdminRoleName))!.UserName
                    : "Тех.поддержка",
                ImageId = isAdmin
                    ? x.Users.FirstOrDefault(y => y.Roles
                        .Select(z => z.Name)
                        .Any(z => z != BaseRoles.AdminRoleName))!.UserPhotoId
                    : null,
                LastMessage = x.Messages
                    .OrderByDescending(y => y.CreatedAt)
                    .FirstOrDefault()!.MessageText
            })
            .ToListAsync(cancellationToken);

        return new GetChatsResponse(chats, totalCount);
    }
}