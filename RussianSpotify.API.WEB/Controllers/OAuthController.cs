using MediatR;
using Microsoft.AspNetCore.Mvc;
using RussianSpotify.API.Core.Enums;
using RussianSpotify.API.Core.Requests.OAuth.GoogleCallback;

namespace RussianSpotify.API.WEB.Controllers;

/// <summary>
/// Контроллер отвечающий за аутентификацию и авторизацию с помощью внешних систем
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OAuthController : ControllerBase
{
    /// <summary>
    /// Получить код от google
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="configuration">Конфигурация</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <param name="code">-</param>
    [HttpGet("Google/Callback")]
    public async Task<IActionResult> GoogleCallbackAsync(
        [FromServices] IMediator mediator,
        [FromServices] IConfiguration configuration,
        CancellationToken cancellationToken,
        string code)
    {
        var result = await mediator.Send(
            new PostGoogleCallbackCommand(code),
            cancellationToken);
        
        HttpContext.Response.Cookies.Append(
            BaseCookieOptions.AccessTokenCookieName,
            result.AccessToken,
            BaseCookieOptions.Options);
        
        HttpContext.Response.Cookies.Append(
            BaseCookieOptions.RefreshTokenCookieName,
            result.RefreshToken,
            BaseCookieOptions.Options);
        
        return Redirect(configuration["RedirectUrl"] ?? string.Empty);
    }
}