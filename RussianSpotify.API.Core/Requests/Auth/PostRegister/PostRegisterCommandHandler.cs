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
using RussianSpotify.API.Shared.Domain.Constants;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.Contracts.Requests.Auth.PostRegister;
using Role = RussianSpotify.API.Core.Entities.Role;

namespace RussianSpotify.API.Core.Requests.Auth.PostRegister;

/// <summary>
/// Обработчик для <see cref="PostRegisterCommand"/>
/// </summary>
public class PostRegisterCommandHandler : IRequestHandler<PostRegisterCommand, PostRegisterResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IPasswordService _passwordService;
    private readonly ITokenFactory _tokenFactory;
    private readonly IEmailSender _emailSender;
    private readonly IDistributedCache _cache;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    /// <param name="passwordService">Сервис хеширования паролей</param>
    /// <param name="tokenFactory">Фабрика токенов для почты</param>
    /// <param name="emailSender">Сервис почты</param>
    /// <param name="cache">Кещ</param>
    public PostRegisterCommandHandler(
        IDbContext dbContext,
        IPasswordService passwordService,
        ITokenFactory tokenFactory,
        IEmailSender emailSender,
        IDistributedCache cache)
    {
        _dbContext = dbContext;
        _passwordService = passwordService;
        _tokenFactory = tokenFactory;
        _emailSender = emailSender;
        _cache = cache;
    }

    /// <inheritdoc />
    public async Task<PostRegisterResponse> Handle(PostRegisterCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var isExistSameUser = await _dbContext.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (isExistSameUser)
            throw new EmailAlreadyRegisteredException(AuthErrorMessages.UserWithSameEmail);

        if (request.Role == Roles.AuthorRoleName)
        {
            var authorWithSameUsername = await _dbContext.Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.UserName.ToLower() == request.UserName.ToLower(), cancellationToken);

            if (authorWithSameUsername != null && authorWithSameUsername.Roles.Any(x => x.Id == Roles.AuthorId))
                throw new BadRequestException($"Автор с именем: {request.UserName} уже существует");
        }

        var passwordHash = _passwordService.HashPassword(request.Password);
        
        var user = new User(
            userName: request.UserName,
            email: request.Email,
            passwordHash: passwordHash);
        
        if (request.Role.Equals(Roles.AdminRoleName, StringComparison.OrdinalIgnoreCase))
            throw new UserCannotBeAdminException("Пользователь не может быть админом");
        
        var baseRole = await _dbContext.Roles
            .FirstOrDefaultAsync(x => x.Name == request.Role, cancellationToken)
            ?? throw new EntityNotFoundException<Role>(request.Role);
        
        user.AddRole(baseRole);

        var token = _tokenFactory.GetToken();
        
        var messageTemplate = await EmailTemplateHelper.GetEmailTemplateAsync(
            Templates.SendEmailConfirmationMessage,
            cancellationToken);

        var placeholders = new Dictionary<string, string> { ["{confirmationToken}"] = token };

        var message = messageTemplate.ReplacePlaceholders(placeholders);
        await _cache.SetStringAsync(request.Email, token, cancellationToken);
        
        await _emailSender.SendEmailAsync(
            user.Email,
            message,
            cancellationToken);

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new PostRegisterResponse(request.Email);
    }
}