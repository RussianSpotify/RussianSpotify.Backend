#region

using MediatR;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.Contracts.Requests.OAuth;

#endregion

namespace RussianSpotify.API.Core.Requests.OAuth.GoogleCallback;

/// <summary>
///     Обработчик для <see cref="PostGoogleCallbackCommand" />
/// </summary>
public class PostGoogleCallbackCommandHandler : UserUpsertBase,
    IRequestHandler<PostGoogleCallbackCommand, GetExternalLoginCallbackResponseBase>
{
    private readonly IGoogleService _googleService;

    /// <inheritdoc />
    public PostGoogleCallbackCommandHandler(
        IDbContext dbContext,
        IUserClaimsManager userClaimsManager,
        IJwtGenerator jwtGenerator,
        IGoogleService googleService)
        : base(dbContext, userClaimsManager, jwtGenerator)
    {
        _googleService = googleService;
    }

    /// <inheritdoc />
    public async Task<GetExternalLoginCallbackResponseBase> Handle(PostGoogleCallbackCommand request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var data = await _googleService.ExchangeCodeForTokenAsync(request.Code)
                   ?? throw new ApplicationBaseException("Не получено данных от сервиса Google");

        var userInfo = await _googleService.GetUserInfoAsync(data.AccessToken)
                       ?? throw new ApplicationBaseException("Не получено данных от сервиса Google");

        return await ApplyUserChanges(
            (userInfo.Email, Username: userInfo.Name ?? $"{userInfo.FamilyName} {userInfo.GivenName}"),
            cancellationToken);
    }
}