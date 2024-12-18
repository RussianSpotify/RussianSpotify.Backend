using RussianSpotify.API.Shared.Exceptions.FileExceptions;

namespace RussianSpotify.API.Grpc.Clients.FileClient;

/// <summary>
/// Сервис S3
/// </summary>
public interface IFileServiceClient
{
    /// <summary>
    /// Является ли данный файл картинкой (Image)
    /// </summary>
    /// <param name="contentType">Тип файла</param>
    /// <returns>Результат проверки</returns>
    /// <exception cref="FileInternalException">У файла не указан ContentType</exception>
    bool IsImage(string contentType);

    /// <summary>
    /// Является ли данный файл музыкой (Audio)
    /// </summary>
    /// <param name="contentType">Тип файла</param>
    /// <returns>Результат проверки</returns>
    /// <exception cref="FileInternalException">У файла не указан ContentType</exception>
    bool IsAudio(string contentType);

    /// <summary>
    /// Получить файл по ИД
    /// </summary>
    /// <param name="fileId">ИД файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Файл</returns>
    Task<Models.File> GetFileAsync(
        Guid? fileId,
        CancellationToken cancellationToken = default);

    Task<Models.FileMetadata> GetFileMetadataAsync(Guid? fileId, CancellationToken cancellationToken = default);

    Task<ICollection<Models.FileMetadata>> GetFilesMetadataAsync(IReadOnlyCollection<Guid?> ids,
        CancellationToken cancellationToken = default);

    Task<ICollection<Models.File>> GetFilesAsync(IReadOnlyCollection<Guid?> ids,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Удалить файл из хранилища
    /// </summary>
    /// <param name="fileId">ИД файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteAsync(
        Guid? fileId,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(IReadOnlyCollection<Guid?> filesIds, CancellationToken cancellationToken = default);
}