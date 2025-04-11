#region

using System.Net;

#endregion

namespace RussianSpotify.API.Shared.Exceptions.OAuthExceptions;

/// <summary>
///     Если не найден Claim с типом Email
/// </summary>
public class EmailClaimNotFoundException : ApplicationBaseException
{
    public EmailClaimNotFoundException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }
}