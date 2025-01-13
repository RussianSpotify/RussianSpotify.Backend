#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Interfaces;

#endregion

namespace RussianSpotify.API.Core.Requests.Auth.PostRevokeToken;

/// <summary>
///     Обработчик для <see cref="PostRevokeTokenCommand" />
/// </summary>
public class PostRevokeTokenCommandHandler : IRequestHandler<PostRevokeTokenCommand>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекс текущего пользоавтеля</param>
    public PostRevokeTokenCommandHandler(IDbContext dbContext, IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }

    /// <inheritdoc />
    public async Task Handle(PostRevokeTokenCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _dbContext.Users
                       .FirstOrDefaultAsync(
                           x => x.Email == request.Email && _userContext.CurrentUserId == x.Id,
                           cancellationToken)
                   ?? throw new EntityNotFoundException<User>(request.Email);

        user.RefreshToken = null;
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}