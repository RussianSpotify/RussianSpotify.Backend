using System.ComponentModel.DataAnnotations;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Enums;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.AuthExceptions;
using RussianSpotify.API.Core.Extensions;
using RussianSpotify.API.Core.Models;
using RussianSpotify.Contracts.Requests.Auth.PostLogin;

namespace RussianSpotify.API.Core.Requests.Auth.PostLogin;

/// <summary>
/// Обработчик для <see cref="PostLoginCommand"/>
/// </summary>
public class PostLoginCommandHandler : IRequestHandler<PostLoginCommand, PostLoginResponse>
{
    private readonly IDbContext _dbContext;
    private readonly ITokenFactory _tokenFactory;
    private readonly IEmailSender _emailSender;
    private readonly IPasswordService _passwordService;
    private readonly IUserClaimsManager _userClaimsManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="tokenFactory">Фабрика токенов для почты</param>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    /// <param name="emailSender">Позволяет отправлять письма на почту</param>
    /// <param name="passwordService">Сервис для хеширования паролей</param>
    /// <param name="userClaimsManager">Отвечает за клэймы юзера</param>
    /// <param name="jwtGenerator">Отвечает за генерацию JWT</param>
    /// <param name="distributedCache">Кеш</param>
    public PostLoginCommandHandler(
        ITokenFactory tokenFactory,
        IDbContext dbContext,
        IEmailSender emailSender,
        IPasswordService passwordService,
        IUserClaimsManager userClaimsManager,
        IJwtGenerator jwtGenerator,
        IDistributedCache distributedCache)
    {
        _tokenFactory = tokenFactory;
        _dbContext = dbContext;
        _emailSender = emailSender;
        _passwordService = passwordService;
        _userClaimsManager = userClaimsManager;
        _jwtGenerator = jwtGenerator;
        _distributedCache = distributedCache;
    }

    /// <inheritdoc />
    public async Task<PostLoginResponse> Handle(PostLoginCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var user = await _dbContext.Users
            .Include(x => x.Roles)
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
            ?? throw new EntityNotFoundException<User>(request.Email);

        // if (!user.IsConfirmed)
        // {
        //     var confirmationToken = _tokenFactory.GetToken();
        //
        //     var messageTemplate = await EmailTemplateHelper.GetEmailTemplateAsync(
        //         Templates.SendEmailConfirmationMessage,
        //         cancellationToken);
        //
        //     var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = confirmationToken };
        //
        //     var message = messageTemplate.ReplacePlaceholders(placeholders);
        //     
        //     await _distributedCache.RemoveAsync(request.Email, cancellationToken);
        //     await _distributedCache.SetStringAsync(
        //         request.Email,
        //         confirmationToken,
        //         cancellationToken);
        //
        //     await _emailSender.SendEmailAsync(
        //         user.Email,
        //         message,
        //         cancellationToken);
        //
        //     throw new NotConfirmedEmailException(AuthErrorMessages.NotConfirmedEmail);
        // }

        var isCorrectPassword = _passwordService.VerifyPassword(request.Password, user.PasswordHash);

        if (!isCorrectPassword)
            throw new ValidationException("Пароль или логин неверный");

        var userClaims = _userClaimsManager.GetUserClaims(user);

        user.AccessToken = _jwtGenerator.GenerateToken(userClaims);
        user.RefreshToken = _jwtGenerator.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(TokenConfiguration.RefreshTokenExpiryDays);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new PostLoginResponse(user.AccessToken, user.RefreshToken);
    }
}