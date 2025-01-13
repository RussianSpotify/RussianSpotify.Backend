#region

using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Http;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Shared.Middlewares;

/// <summary>
///     Middleware, отвечающий за обработку ошибок
/// </summary>
public class ExceptionMiddleware : IMiddleware
{
    /// <inheritdoc cref="IMiddleware" />
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ApplicationBaseException exception)
        {
            if (exception.ResponseStatusCode != default)
                context.Response.StatusCode = (int)exception.ResponseStatusCode;

            await context.Response.WriteAsJsonAsync(new { message = exception.Message });
        }
        catch (ValidationException exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            await context.Response.WriteAsJsonAsync(new { message = exception.Message });
        }
        catch (Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsJsonAsync(new { message = exception.Message });
        }
    }
}