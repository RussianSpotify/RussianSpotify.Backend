using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Extensions;
using RussianSpotify.API.Core.Models;
using RussianSpotify.Contracts.Requests.Auth.PostResetPassword;

namespace RussianSpotify.API.Core.Requests.Auth.PostResetPassword;

/// <summary>
/// Обработчик для <see cref="PostResetPasswordCommand"/>
/// </summary>
public class PostResetPasswordCommandHandler : IRequestHandler<PostResetPasswordCommand, PostResetPasswordResponse>
{
    private const string Prefix = "Reset_";
    
    private readonly IDbContext _dbContext;
    private readonly IEmailSender _emailSender;
    private readonly IDistributedCache _distributedCache;
    private readonly ITokenFactory _tokenFactory;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    /// <param name="emailSender">Позволяет отправлять письма на почту</param>
    /// <param name="distributedCache">Кеш</param>
    /// <param name="tokenFactory">Фабрика токенов для почты</param>
    public PostResetPasswordCommandHandler(
        IDbContext dbContext,
        IEmailSender emailSender,
        IDistributedCache distributedCache,
        ITokenFactory tokenFactory)
    {
        _dbContext = dbContext;
        _emailSender = emailSender;
        _distributedCache = distributedCache;
        _tokenFactory = tokenFactory;
    }

    /// <inheritdoc />
    public async Task<PostResetPasswordResponse> Handle(PostResetPasswordCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _dbContext.Users
            .AnyAsync(x => request.Email == x.Email, cancellationToken);

        if (!user)
            throw new EntityNotFoundException<User>(request.Email);
        
        var confirmToken = _tokenFactory.GetToken();
        
        var messageTemplate = await EmailTemplateHelper.GetEmailTemplateAsync(
            Templates.SendPasswordResetConfirmationMessage,
            cancellationToken);

        var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = confirmToken };
        
        var message = messageTemplate.ReplacePlaceholders(placeholders);

        await _distributedCache.SetStringAsync(
            $"{Prefix}{request.Email}",
            confirmToken,
            cancellationToken);
        
        await _emailSender.SendEmailAsync(request.Email, message, cancellationToken);

        return new PostResetPasswordResponse(request.Email);
    }
}