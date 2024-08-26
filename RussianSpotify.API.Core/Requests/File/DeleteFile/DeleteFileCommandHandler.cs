using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.DefaultSettings;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Core.Exceptions.FileExceptions;

namespace RussianSpotify.API.Core.Requests.File.DeleteFile;

/// <summary>
/// Обработчик команды <see cref="DeleteFileCommand"/>
/// </summary>
public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly IFileHelper _fileHelper;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст базы данных</param>
    /// <param name="fileHelper">Сервис-помощник для работы с файлами в S3</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public DeleteFileCommandHandler(
        IDbContext dbContext,
        IUserContext userContext,
        IFileHelper fileHelper)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _fileHelper = fileHelper;
    }

    /// <inheritdoc/>
    public async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var fileFromDb = await _dbContext.Files
            .FirstOrDefaultAsync(i => i.Id == request.FileId, cancellationToken)
            ?? throw new EntityNotFoundException<Entities.File>(request.FileId);

        if (!_userContext.CurrentUserId.HasValue)
            throw new ForbiddenException();

        if (fileFromDb.UserId != _userContext.CurrentUserId && _userContext.RoleName != BaseRoles.AdminRoleName)
            throw new FileBadRequestException("You cant delete this file!");

        await _fileHelper.DeleteFileAsync(fileFromDb, cancellationToken);
    }
}