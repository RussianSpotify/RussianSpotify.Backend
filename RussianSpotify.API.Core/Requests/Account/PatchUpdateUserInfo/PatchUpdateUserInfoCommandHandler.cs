#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Enums;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.AuthExceptions;
using RussianSpotify.API.Core.Models;
using RussianSpotify.API.Grpc.Clients.FileClient;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Account.PatchUpdateUserInfo;

#endregion

namespace RussianSpotify.API.Core.Requests.Account.PatchUpdateUserInfo;

/// <summary>
///     Обработчик для <see cref="PatchUpdateUserInfoCommand" />
/// </summary>
public class PatchUpdateUserInfoCommandHandler
    : IRequestHandler<PatchUpdateUserInfoCommand, PatchUpdateUserInfoResponse>
{
    private readonly IUserContext _userContext;
    private readonly IUserClaimsManager _claimsManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IEmailSender _emailSender;
    private readonly IFileServiceClient _fileServiceClient;
    private readonly IDbContext _dbContext;
    private readonly IPasswordChanger _passwordChanger;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="userContext">UserContext <see cref="IUserContext" /></param>
    /// <param name="claimsManager">Claims Manager <see cref="IUserClaimsManager" />/></param>
    /// <param name="jwtGenerator">Генератор JWT токенов</param>
    /// <param name="emailSender">Email sender <see cref="IEmailSender" /></param>
    /// <param name="fileServiceClient">Сервис-помощник для работы с файлами</param>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    /// <param name="passwordChanger">Сервис по смене пароля</param>
    public PatchUpdateUserInfoCommandHandler(
        IUserContext userContext,
        IUserClaimsManager claimsManager,
        IJwtGenerator jwtGenerator,
        IEmailSender emailSender,
        IFileServiceClient fileServiceClient,
        IDbContext dbContext,
        IPasswordChanger passwordChanger)
    {
        _userContext = userContext;
        _claimsManager = claimsManager;
        _jwtGenerator = jwtGenerator;
        _emailSender = emailSender;
        _fileServiceClient = fileServiceClient;
        _dbContext = dbContext;
        _passwordChanger = passwordChanger;
    }

    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}" />
    public async Task<PatchUpdateUserInfoResponse> Handle(PatchUpdateUserInfoCommand request,
        CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await _dbContext.Users
                       .Include(i => i.Roles)
                       .FirstOrDefaultAsync(i => i.Id == _userContext.CurrentUserId, cancellationToken)
                   ?? throw new ForbiddenException();

        user.UserName = !string.IsNullOrWhiteSpace(request.UserName)
            ? request.UserName
            : user.UserName;

        if (request.FilePhotoId is not null)
        {
            // Достаем картину из бд
            var file = await _fileServiceClient
                .GetFileMetadataAsync(request.FilePhotoId.Value, cancellationToken);

            // Проверка, является ли файл картинкой и присвоение
            if (!_fileServiceClient.IsImage(file.ContentType))
                throw new UserBadImageException("File's content type is not Image");

            // Удаляем текущую картинку
            if (user.UserPhotoId is not null)
                await _fileServiceClient.DeleteAsync(user.UserPhotoId, cancellationToken);

            user.UserPhotoId = request.FilePhotoId.Value;
        }

        var claims = _claimsManager.GetUserClaims(user);

        user.AccessToken = _jwtGenerator.GenerateToken(claims);
        user.RefreshToken = _jwtGenerator.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(TokenConfiguration.RefreshTokenExpiryDays);

        var message = await EmailTemplateHelper.GetEmailTemplateAsync(
            Templates.SendUserInfoUpdatedNotification,
            cancellationToken);

        if (!string.IsNullOrWhiteSpace(request.NewPassword)
            && !string.IsNullOrWhiteSpace(request.NewPasswordConfirm)
            && request.NewPasswordConfirm!.Equals(request.NewPassword, StringComparison.Ordinal))
            _passwordChanger.ChangePassword(user, request.CurrentPassword!, request.NewPassword);

        await _emailSender.SendEmailAsync(user.Email, message, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PatchUpdateUserInfoResponse(user.AccessToken, user.RefreshToken);
    }
}