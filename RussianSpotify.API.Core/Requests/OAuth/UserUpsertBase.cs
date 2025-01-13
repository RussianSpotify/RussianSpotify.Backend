#region

using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Domain.Constants;
using RussianSpotify.Contracts.Requests.OAuth;

#endregion

namespace RussianSpotify.API.Core.Requests.OAuth;

/// <summary>
///     Базовый класс для работы с обновлением/созданием пользователя при входе через сторонние сервисы
/// </summary>
public abstract class UserUpsertBase
{
    private readonly IDbContext _dbContext;
    private readonly IUserClaimsManager _userClaimsManager;
    private readonly IJwtGenerator _jwtGenerator;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    /// <param name="userClaimsManager">Отвечает за клэймы юзера</param>
    /// <param name="jwtGenerator">Отвечает за генерацию JWT</param>
    protected UserUpsertBase(
        IDbContext dbContext,
        IUserClaimsManager userClaimsManager,
        IJwtGenerator jwtGenerator)
    {
        _dbContext = dbContext;
        _userClaimsManager = userClaimsManager;
        _jwtGenerator = jwtGenerator;
    }

    /// <summary>
    ///     Применить изменения к пользователю
    /// </summary>
    /// <param name="userInfo">Ответ от внешней системы</param>
    /// <param name="cancellationToken">Токен отмены</param>
    protected async Task<GetExternalLoginCallbackResponseBase> ApplyUserChanges(
        (string? Email, string Username) userInfo,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(userInfo);

        if (string.IsNullOrWhiteSpace(userInfo.Email))
            throw new RequiredFieldException("Почта");

        var userExist = await _dbContext.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == userInfo.Email, cancellationToken);

        if (userExist == null)
        {
            var role = await _dbContext.Roles
                           .FirstOrDefaultAsync(x => x.Name == Roles.UserRoleName, cancellationToken)
                       ?? throw new EntityNotFoundException<Role>(Roles.UserRoleName);

            userExist = new User(
                userName: userInfo.Username,
                email: userInfo.Email,
                isConfirmed: true,
                passwordHash: Guid.NewGuid().ToString(),
                roles: new List<Role>
                {
                    role
                });

            _dbContext.Users.Add(userExist);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        var userClaims = _userClaimsManager.GetUserClaims(userExist);

        var accessToken = _jwtGenerator.GenerateToken(userClaims);
        var refreshToken = _jwtGenerator.GenerateRefreshToken();

        userExist.AccessToken = accessToken;
        userExist.RefreshToken = refreshToken;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new GetExternalLoginCallbackResponseBase(accessToken, refreshToken);
    }
}