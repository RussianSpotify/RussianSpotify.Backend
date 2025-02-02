#region

using System.Net;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.OAuthExceptions;

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