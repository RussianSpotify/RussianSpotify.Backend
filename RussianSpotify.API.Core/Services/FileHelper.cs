using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Exceptions.FileExceptions;
using File = RussianSpotify.API.Core.Entities.File;

namespace RussianSpotify.API.Core.Services;

/// <inheritdoc/>
public class FileHelper : IFileHelper
{
    private const string ImageFileStartsWith = "image/";
    private const string AudioFileStartsWith = "audio/";
    
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
    public bool IsImage(File file)
    {
        var fileContentType = file.ContentType;
        if (fileContentType is null)
            throw new FileInternalException("File's content type not set");

        return fileContentType.StartsWith(ImageFileStartsWith);
    }

    /// <inheritdoc/>
    public bool IsAudio(File file)
    {
        var fileContentType = file.ContentType;
        if (fileContentType is null)
            throw new FileInternalException("File's content type not set");

        return fileContentType.StartsWith(AudioFileStartsWith);
    }

    /// <inheritdoc/>
    public async Task DeleteFileAsync(File file, CancellationToken cancellationToken)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));

        if (file.Address is null)
            throw new FileInternalException("File Address not set");

        await _s3Service.DeleteAsync(file.Address, cancellationToken: cancellationToken);
        _dbContext.Files.Remove(file);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}