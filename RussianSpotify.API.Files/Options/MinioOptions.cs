namespace RussianSpotify.API.Files.Options;

/// <summary>
/// Настройки для Minio S3
/// </summary>
public class MinioOptions
{
    /// <summary>
    /// Название клиента
    /// </summary>
    public string MinioClient { get; set; } = default!;
    
    /// <summary>
    /// Логин
    /// </summary>
    public string AccessKey { get; set; } = default!;

    /// <summary>
    /// Секрет
    /// </summary>
    public string SecretKey { get; set; } = default!;

    /// <summary>
    /// Url хранилища
    /// </summary>
    public string ServiceUrl { get; set; } = default!;

    /// <summary>
    /// Название основного бакета (постоянное хранилище)
    /// </summary>
    public string BucketName { get; set; } = default!;

    /// <summary>
    /// Название временного бакета (временное хранилище)
    /// </summary>
    public string TempBucketName { get; set; } = default!;

    /// <summary>
    /// Таймаут
    /// </summary>
    public TimeSpan TimeOut { get; set; } = TimeSpan.FromMinutes(3);
}