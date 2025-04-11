#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions;

/// <summary>
///     Ошибка 403
/// </summary>
public class ForbiddenException : ApplicationBaseException
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    public ForbiddenException()
        : base("Вход воспрещен", HttpStatusCode.Forbidden)
    {
    }

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="message">Сообщение</param>
    public ForbiddenException(string message)
        : base(message, HttpStatusCode.Forbidden)
    {
    }
}