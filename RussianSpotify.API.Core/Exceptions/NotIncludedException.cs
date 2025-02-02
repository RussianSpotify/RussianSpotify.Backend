#region

using System.Net;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions;

/// <summary>
///     Ошибка об забытом Include
/// </summary>
public class NotIncludedException : ApplicationBaseException
{
    /// <summary>
    ///     Консутрктор
    /// </summary>
    /// <param name="message"></param>
    /// <param name="statusCode"></param>
    public NotIncludedException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base($"Забыли include на файл: {message}", statusCode)
    {
    }

    /// <summary>
    ///     Конструктор
    /// </summary>
    public NotIncludedException()
    {
    }
}