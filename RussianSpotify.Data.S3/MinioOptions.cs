namespace RussianSpotify.Data.S3;

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
    /// Название бакета
    /// </summary>
    public string BucketName { get; set; } = default!;

    /// <summary>
    /// Таймаут
    /// </summary>
    public TimeSpan TimeOut { get; set; } = TimeSpan.FromMinutes(3);
}