#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Exceptions;
using ValidationException = FluentValidation.ValidationException;

#endregion

namespace RussianSpotify.API.Core.Requests.Auth.PostConfirmEmail;

/// <summary>
///     Обработчик для <see cref="PostConfirmEmailCommand" />
/// </summary>
public class PostConfirmEmailCommandHandler : IRequestHandler<PostConfirmEmailCommand>
{
    private readonly IDbContext _dbContext;
    private readonly IDistributedCache _distributedCache;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="distributedCache">Кеш</param>
    /// <param name="dbContext">Контекст БД</param>
    public PostConfirmEmailCommandHandler(IDistributedCache distributedCache, IDbContext dbContext)
    {
        _distributedCache = distributedCache;
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task Handle(PostConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.EmailVerificationCodeFromUser))
            throw new RequiredFieldException("Код");

        var user = await _dbContext.Users
                       .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken)
                   ?? throw new EntityNotFoundException<User>(request.Email);

        var exceptedCode = await _distributedCache
                               .GetStringAsync(request.Email, cancellationToken)
                           ?? throw new NotFoundException("Код для данного пользователя не найден");

        if (!exceptedCode.Equals(request.EmailVerificationCodeFromUser))
            throw new ValidationException("Введен неверный код");

        user.IsConfirmed = true;
        await _distributedCache.RemoveAsync(request.Email, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}