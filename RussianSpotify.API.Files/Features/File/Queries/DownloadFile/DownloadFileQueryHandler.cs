using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Files.Data;
using RussianSpotify.API.Files.Domain.Entities;
using RussianSpotify.API.Files.Exceptions;
using RussianSpotify.API.Files.Interfaces;
using RussianSpotify.API.Files.Models;
using RussianSpotify.API.Files.Requests.File.DownloadFile;

namespace RussianSpotify.API.Files.Features.File.Queries.DownloadFile;

/// <summary>
/// Обработчик для <see cref="DownloadFileQuery"/>
/// </summary>
public class DownloadFileQueryHandler : IRequestHandler<DownloadFileQuery, DownloadFileResponse>
{
    private readonly IS3Service _s3Service;
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="s3Service">S3 сервис</param>
    /// <param name="dbContext">Контекст БД</param>
    public DownloadFileQueryHandler(IS3Service s3Service, IDbContext dbContext)
    {
        _s3Service = s3Service;
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<DownloadFileResponse> Handle(DownloadFileQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var fileFromDb = await _dbContext.FilesMetadata
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            ?? throw new EntityNotFoundException<FileMetadata>(request.Id);

        var file = await _s3Service
            .GetFileAsync(fileFromDb.Address, cancellationToken: cancellationToken)
            ?? throw new EntityNotFoundException<FileContent>(fileFromDb.Address);

        return new DownloadFileResponse(
            content: file.Content,
            fileName: fileFromDb.FileName ?? file.FileName,
            contentType: file.ContentType);
    }
}