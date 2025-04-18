﻿#region

using Microsoft.AspNetCore.Http;

#endregion

namespace RussianSpotify.API.Core.Enums;

/// <summary>
///     Базовые настройки Cookies
/// </summary>
public static class BaseCookieOptions
{
    /// <summary>
    ///     Cookie Options
    /// </summary>
    public static readonly CookieOptions Options = new()
    {
        Expires = DateTime.UtcNow.AddHours(5),
    };

    /// <summary>
    ///     Name куки для JWT
    /// </summary>
    public const string AccessTokenCookieName = "token";

    /// <summary>
    ///     Name куки для Refresh Токена
    /// </summary>
    public const string RefreshTokenCookieName = "refresh";
}