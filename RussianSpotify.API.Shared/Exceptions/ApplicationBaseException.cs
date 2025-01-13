#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions;

/// <summary>
///     Базовый класс ошибок
/// </summary>
public class ApplicationBaseException : Exception
{
    public HttpStatusCode ResponseStatusCode { get; set; }

    /// <summary>
    ///     Конструктор
    /// </summary>
    public ApplicationBaseException()
    {
    }

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="message">Сообщение об ошибку</param>
    public ApplicationBaseException(string message)
    {
    }

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="message">Сообщение об ошибке</param>
    /// <param name="statusCode">Код ошибки</param>
    public ApplicationBaseException(string message, HttpStatusCode statusCode)
        : base(message)
    {
        ResponseStatusCode = statusCode;
    }
}