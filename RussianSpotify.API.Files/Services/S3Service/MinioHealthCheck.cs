using Microsoft.Extensions.Diagnostics.HealthChecks;
using Minio;
using Minio.DataModel.Args;
using RussianSpotify.API.Files.Options;

namespace RussianSpotify.API.Files.Services.S3Service;

/// <summary>
/// Проверка жизни сервиса S3
/// </summary>
public class MinioHealthCheck : IHealthCheck
{
    private readonly IMinioClient _minioClient;
    private readonly MinioOptions _minioOptions;
    private readonly ILogger<MinioHealthCheck> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="minioClient">Клиент Minio</param>
    /// <param name="logger"></param>
    /// <param name="minioOptions">Настройки minio</param>
    public MinioHealthCheck(IMinioClient minioClient, ILogger<MinioHealthCheck> logger, MinioOptions minioOptions)
    {
        _minioClient = minioClient;
        _logger = logger;
        _minioOptions = minioOptions;
    }

    /// <inheritdoc />
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        try
        {
            _logger.LogInformation("Checking health for Minio bucket: {BucketName}", _minioOptions.BucketName);
            
            var exists = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_minioOptions.BucketName), cancellationToken);
            if (exists)
            {
                _logger.LogInformation("Minio bucket {BucketName} is healthy", _minioOptions.BucketName);
                return HealthCheckResult.Healthy($"Bucket '{_minioOptions.BucketName}' exists.");
            }

            _logger.LogWarning("Minio bucket {BucketName} does not exist", _minioOptions.BucketName);
            return HealthCheckResult.Unhealthy($"Bucket '{_minioOptions.BucketName}' does not exist.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking health for Minio bucket: {BucketName}", _minioOptions.BucketName);
            return HealthCheckResult.Unhealthy($"Exception during health check: {ex.Message}");
        }
    }
}