#region

using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Enums;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Exceptions.AuthExceptions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Auth.PostRefreshToken;

#endregion

namespace RussianSpotify.API.Core.Requests.Auth.PostRefreshToken;

/// <summary>
///     Обработчик для <see cref="PostRefreshTokenCommand" />
/// </summary>
public class PostRefreshTokenCommandHandler : IRequestHandler<PostRefreshTokenCommand, PostRefreshTokenResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IDateTimeProvider _dateTimeProvider;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="jwtGenerator">Отвечает за генерацию JWT</param>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    /// <param name="dateTimeProvider">Провайдер даты</param>
    public PostRefreshTokenCommandHandler(
        IJwtGenerator jwtGenerator,
        IDbContext dbContext,
        IDateTimeProvider dateTimeProvider)
    {
        _jwtGenerator = jwtGenerator;
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    /// <inheritdoc />
    public async Task<PostRefreshTokenResponse> Handle(PostRefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var principal = _jwtGenerator
                            .GetPrincipalFromExpiredToken(request.AccessToken)
                        ?? throw new InvalidTokenException(AuthErrorMessages.InvalidAccessToken);

        var userEmail = principal.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;

        if (string.IsNullOrWhiteSpace(userEmail))
            throw new NotFoundException("Не найдена почта");

        var user = await _dbContext.Users
                       .FirstOrDefaultAsync(x => x.Email == userEmail, cancellationToken)
                   ?? throw new EntityNotFoundException<User>(userEmail);

        if (user.RefreshToken != request.RefreshToken
            || user.RefreshTokenExpiryTime <= _dateTimeProvider.CurrentDate)
            throw new InvalidTokenException(AuthErrorMessages.InvalidRefreshToken);

        var newAccessToken = _jwtGenerator.GenerateToken(principal.Claims.ToList());
        var newRefreshToken = _jwtGenerator.GenerateRefreshToken();

        user.AccessToken = newAccessToken;
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = _dateTimeProvider.CurrentDate.AddDays(TokenConfiguration.RefreshTokenExpiryDays);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new PostRefreshTokenResponse(user.AccessToken, user.RefreshToken);
    }
}