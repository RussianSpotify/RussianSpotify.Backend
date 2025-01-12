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
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        try
        {
            _logger.LogInformation("Checking health for Minio buckets: {BucketName} (main), {TempBucketName} (temp)",
                _minioOptions.BucketName, _minioOptions.TempBucketName);

            var mainBucketExists = await _minioClient.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(_minioOptions.BucketName), cancellationToken);

            var tempBucketExists = await _minioClient.BucketExistsAsync(
                new BucketExistsArgs().WithBucket(_minioOptions.TempBucketName), cancellationToken);

            if (mainBucketExists && tempBucketExists)
            {
                _logger.LogInformation("Minio buckets are healthy: {BucketName} (main), {TempBucketName} (temp)",
                    _minioOptions.BucketName, _minioOptions.TempBucketName);
                return HealthCheckResult.Healthy(
                    $"Buckets '{_minioOptions.BucketName}' (main) and '{_minioOptions.TempBucketName}' (temp) exist.");
            }

            if (!mainBucketExists)
            {
                _logger.LogWarning("Minio bucket {BucketName} (main) does not exist", _minioOptions.BucketName);
            }

            if (!tempBucketExists)
            {
                _logger.LogWarning("Minio bucket {TempBucketName} (temp) does not exist", _minioOptions.TempBucketName);
            }

            return HealthCheckResult.Unhealthy(
                $"Bucket '{_minioOptions.BucketName}' (main) exists: {mainBucketExists}, " +
                $"Bucket '{_minioOptions.TempBucketName}' (temp) exists: {tempBucketExists}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error checking health for Minio buckets: {BucketName} (main), {TempBucketName} (temp)",
                _minioOptions.BucketName, _minioOptions.TempBucketName);
            return HealthCheckResult.Unhealthy($"Exception during health check: {ex.Message}");
        }
    }
}