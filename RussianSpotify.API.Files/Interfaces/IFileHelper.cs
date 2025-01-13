#region

using RussianSpotify.API.Files.Domain.Entities;

#endregion

namespace RussianSpotify.API.Files.Interfaces;

/// <summary>
///     Сервис-помощник для работы с файлами
/// </summary>
public interface IFileHelper
{
    /// <summary>
    ///     Удалить файл из бд и из хранилища
    /// </summary>
    /// <param name="file">Файл для удаления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    Task DeleteFileAsync(FileMetadata file, CancellationToken cancellationToken);
}