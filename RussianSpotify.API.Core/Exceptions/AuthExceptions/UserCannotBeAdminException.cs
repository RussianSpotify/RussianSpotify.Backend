﻿#region

using System.Net;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions.AuthExceptions;

public class UserCannotBeAdminException : ApplicationBaseException
{
    public UserCannotBeAdminException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        : base(message, statusCode)
    {
    }

    public UserCannotBeAdminException()
    {
    }
}