using RussianSpotify.API.Files.Data;
using RussianSpotify.API.Files.Domain.Entities;
using RussianSpotify.API.Files.Exceptions.FileExceptions;
using RussianSpotify.API.Files.Interfaces;

namespace RussianSpotify.API.Files.Services;

/// <inheritdoc/>
public class FileHelper : IFileHelper
{   
    private readonly IS3Service _s3Service;
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="s3Service">Сервис S3</param>
    /// <param name="dbContext">Контекст БД</param>
    public FileHelper(IS3Service s3Service, IDbContext dbContext)
    {
        _s3Service = s3Service;
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task DeleteFileAsync(FileMetadata file, CancellationToken cancellationToken)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));

        if (file.Address is null)
            throw new FileInternalException("File Address not set");

        await _s3Service.DeleteAsync(file.Address, cancellationToken: cancellationToken);
        _dbContext.FilesMetadata.Remove(file);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}