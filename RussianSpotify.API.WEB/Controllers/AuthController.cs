#region

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RussianSpotify.API.Core.Requests.Auth.PostConfirmEmail;
using RussianSpotify.API.Core.Requests.Auth.PostConfirmPasswordReset;
using RussianSpotify.API.Core.Requests.Auth.PostLogin;
using RussianSpotify.API.Core.Requests.Auth.PostRefreshToken;
using RussianSpotify.API.Core.Requests.Auth.PostRegister;
using RussianSpotify.API.Core.Requests.Auth.PostResetPassword;
using RussianSpotify.API.Core.Requests.Auth.PostRevokeToken;
using RussianSpotify.Contracts.Requests.Auth.PostConfirmEmail;
using RussianSpotify.Contracts.Requests.Auth.PostConfirmPasswordReset;
using RussianSpotify.Contracts.Requests.Auth.PostLogin;
using RussianSpotify.Contracts.Requests.Auth.PostRefreshToken;
using RussianSpotify.Contracts.Requests.Auth.PostRegister;
using RussianSpotify.Contracts.Requests.Auth.PostResetPassword;
using RussianSpotify.Contracts.Requests.Auth.PostRevokeToken;

#endregion

namespace RussianSpotify.API.WEB.Controllers;

/// <summary>
///     Контроллер отвечающий за авторизацию и регистрацию
/// </summary>
[ApiController]
[Route("api/[controller]/")]
public class AuthController : ControllerBase
{
    /// <summary>
    ///     Регистрация пользователя
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("Register")]
    [ProducesResponseType(type: typeof(PostRegisterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<PostRegisterResponse> RegisterAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostRegisterRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostRegisterCommand(request), cancellationToken);

    /// <summary>
    ///     Войти в систему
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Пара токенов</returns>
    [HttpPost("Login")]
    [ProducesResponseType(type: typeof(PostLoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<PostLoginResponse> LoginAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostLoginRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostLoginCommand(request), cancellationToken);

    /// <summary>
    ///     Подтверждение почты
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("ConfirmEmail")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task ConfirmEmailAsync(
        [FromServices] IMediator mediator,
        [FromBody] PostConfirmEmailRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostConfirmEmailCommand(request), cancellationToken);

    /// <summary>
    ///     Обновление JWT
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("RefreshToken")]
    [ProducesResponseType(type: typeof(PostRefreshTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<PostRefreshTokenResponse> RefreshToken(
        [FromServices] IMediator mediator,
        [FromBody] PostRefreshTokenRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostRefreshTokenCommand(request), cancellationToken);

    /// <summary>
    ///     Удаление Refresh Token
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [Authorize]
    [HttpPost("RevokeToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    public async Task RevokeToken(
        [FromServices] IMediator mediator,
        [FromBody] PostRevokeTokenRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostRevokeTokenCommand(request), cancellationToken);

    /// <summary>
    ///     Сброс пароля
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("ResetPassword")]
    [ProducesResponseType(type: typeof(PostResetPasswordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<PostResetPasswordResponse> ResetPassword(
        [FromServices] IMediator mediator,
        [FromBody] PostResetPasswordRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostResetPasswordCommand(request), cancellationToken);

    /// <summary>
    ///     Подтверждение сброса пароля
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpPost("ConfirmPasswordReset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task ConfirmPasswordReset(
        [FromServices] IMediator mediator,
        [FromBody] PostConfirmPasswordResetRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new PostConfirmPasswordResetCommand(request), cancellationToken);
}