using MediatR;
using RussianSpotify.API.Files.Data;
using RussianSpotify.API.Files.Domain.Entities;
using RussianSpotify.API.Files.Interfaces;
using RussianSpotify.API.Files.Models;
using RussianSpotify.API.Files.Requests.File.UploadFile;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Interfaces;

namespace RussianSpotify.API.Files.Features.File.Commands.UploadFile;

/// <summary>
/// Обработчик для <see cref="UploadFileCommand"/>
/// </summary>
public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, UploadFileResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IS3Service _s3Service;
    private readonly IUserContext _userContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="s3Service">Сервис S3</param>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userContext">Контекст текущего пользователя</param>
    public UploadFileCommandHandler(
        IS3Service s3Service,
        IDbContext dbContext,
        IUserContext userContext)
    {
        _s3Service = s3Service;
        _dbContext = dbContext;
        _userContext = userContext;
    }

    /// <inheritdoc />
    public async Task<UploadFileResponse> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var filesToSave = new List<FileMetadata>();
        foreach (var file in request.Files)
        {
            if (string.IsNullOrWhiteSpace(file.FileName))
                throw new ArgumentNullException(nameof(file.FileName));

            if (file.FileStream.Length <= 0)
                throw new ArgumentException($"Некоректное кол-во байт");

            var address = await _s3Service.UploadAsync(
                fileContent: new FileContent
                {
                    Content = file.FileStream,
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FileSize = file.FileStream.Length,
                    UploadedBy = _userContext.CurrentUserId ?? throw new CurrentUserIdNotFound("UserId из Claims не был найден"),
                    CreatedAt = DateTime.UtcNow
                },
                cancellationToken: cancellationToken);

            filesToSave.Add(new FileMetadata(
                userId: _userContext.CurrentUserId ?? throw new CurrentUserIdNotFound("UserId из Claims не был найден"),
                fileName: file.FileName,
                contentType: file.ContentType,
                address: address,
                size: file.FileStream.Length));
        }

        await _dbContext.FilesMetadata.AddRangeAsync(filesToSave, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new UploadFileResponse(
            filesToSave
                .Select(x => new UploadFileResponseItem(
                    x.FileName ?? string.Empty,
                    x.Id)));
    }
}