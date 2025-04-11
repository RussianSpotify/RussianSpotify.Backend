#region

using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Requests.Auth.PostConfirmPasswordReset;

/// <summary>
///     Обработчик для <see cref="PostConfirmPasswordResetCommand" />
/// </summary>
public class PostConfirmPasswordResetCommandHandler : IRequestHandler<PostConfirmPasswordResetCommand>
{
    private const string Prefix = "Reset_";

    private readonly IDbContext _dbContext;
    private readonly IDistributedCache _distributedCache;
    private readonly IPasswordService _passwordService;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="distributedCache">Кеш</param>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    /// <param name="passwordService">Сервис для хеширования паролей</param>
    public PostConfirmPasswordResetCommandHandler(
        IDistributedCache distributedCache,
        IDbContext dbContext,
        IPasswordService passwordService)
    {
        _distributedCache = distributedCache;
        _dbContext = dbContext;
        _passwordService = passwordService;
    }

    /// <inheritdoc />
    public async Task Handle(PostConfirmPasswordResetCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _dbContext.Users
                       .FirstOrDefaultAsync(x => request.Email == x.Email, cancellationToken)
                   ?? throw new EntityNotFoundException<User>(request.Email);

        var code = await _distributedCache
            .GetStringAsync($"{Prefix}{request.Email}", cancellationToken);

        if (code is null)
            throw new NotFoundException("Код не найден.");

        if (!request.VerificationCodeFromUser.Equals(code))
            throw new ValidationException("Код неверный");

        var newHash = _passwordService.HashPassword(request.NewPassword);
        user.PasswordHash = newHash;
        user.RefreshToken = null;

        await _distributedCache.RemoveAsync($"{Prefix}{request.Email}", cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}