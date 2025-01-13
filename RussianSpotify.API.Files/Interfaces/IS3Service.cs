using RussianSpotify.API.Files.Domain.Entities;
using RussianSpotify.API.Files.Models;

namespace RussianSpotify.API.Files.Interfaces;

/// <summary>
/// Сервис S3
/// </summary>
public interface IS3Service
{
    /// <summary>
    /// Загрузить файл в хранилище
    /// </summary>
    /// <param name="fileContent">Бинарные данные</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Метадата</returns>
    Task<FileMetadata> UploadAsync(
        FileContent fileContent,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Скачать файл по ключу
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="bucket">Бакет если отличется от умолчания</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Файл</returns>
    Task<FileContent?> GetFileAsync(
        string key,
        string? bucket = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить файл в виде url
    /// </summary>
    /// <param name="key">Адрес</param>
    /// <param name="bucket">Бакет</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>URL</returns>
    Task<string> GetFileUrlAsync(
        string key,
        string? bucket = default,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Удалить файл из хранилища
    /// </summary>
    /// <param name="key">Ключ</param>
    /// <param name="bucket">Бакет если отличется от умолчания</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteAsync(
        string key,
        string? bucket = default,
        CancellationToken cancellationToken = default);
}