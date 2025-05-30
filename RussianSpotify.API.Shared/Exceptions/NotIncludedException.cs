#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions;

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