using Microsoft.Extensions.Caching.Distributed;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Newtonsoft.Json;
using RussianSpotify.API.Files.Interfaces;
using RussianSpotify.API.Files.Models;

namespace RussianSpotify.API.Files.Services.S3Service;

/// <inheritdoc />
public class S3Service: IS3Service
{
    private const string DefaultContentType = "application/octet-stream";

    private readonly Options.MinioOptions _minioOptions;
    private readonly ILogger<S3Service> _logger;
    private readonly IMinioClient _minioClient;
    private readonly IDistributedCache _redis;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="minioOptions">Настройки для S3</param>
    /// <param name="logger">Логгер</param>
    /// <param name="minioClient">Клиент Minio</param>
    /// <param name="redis">Редис</param>
    public S3Service(
        Options.MinioOptions minioOptions,
        ILogger<S3Service> logger,
        IMinioClient minioClient, IDistributedCache redis)
    {
        _minioOptions = minioOptions;
        _logger = logger;
        _minioClient = minioClient;
        _redis = redis;
    }
    
    /// <inheritdoc />
    public async Task<string> UploadAsync(
        FileContent fileContent,
        CancellationToken cancellationToken = default)
    {
        try
        {
            if (fileContent.FileName == null)
                throw new ArgumentNullException(nameof(fileContent.FileName));
    
            if (fileContent.Content == null)
                throw new ArgumentNullException(nameof(fileContent.Content));

            var minioFileLocation = ContentKey(fileContent.FileName);

            await BucketExistAsync(_minioOptions.TempBucketName, cancellationToken);
        
            var putArgs = new PutObjectArgs()
                .WithBucket(_minioOptions.TempBucketName)
                .WithObject(minioFileLocation)
                .WithStreamData(fileContent.Content)
                .WithObjectSize(fileContent.Content.Length);

            await _minioClient.PutObjectAsync(putArgs, cancellationToken);

            // Сохранение метаданных в Redis
            var metadataKey = $"metadata:{minioFileLocation}";
            var metadata = new FileMetadataDto
            {
                FileName = fileContent.FileName,
                FileSize = fileContent.FileSize,
                ContentType = fileContent.ContentType,
                UploadedBy = fileContent.UploadedBy,
                CreatedAt = fileContent.CreatedAt
            };

            var metadataJson = JsonConvert.SerializeObject(metadata);

            await _redis.SetStringAsync(metadataKey, metadataJson, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            }, cancellationToken);

            // Обработка счетчика загрузок
            var counterKey = $"counter:{minioFileLocation}";
            var counter = await _redis.GetStringAsync(counterKey, cancellationToken);
            var newCounter = counter == null ? 1 : int.Parse(counter) + 1;

            await _redis.SetStringAsync(counterKey, newCounter.ToString(), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
            }, cancellationToken);

            // Перенос в постоянное хранилище
            if (newCounter == 2)
            {
                // Проверка успешного сохранения метаданных
                var savedMetadata = await _redis.GetStringAsync(metadataKey, cancellationToken);
                if (!string.IsNullOrEmpty(savedMetadata))
                {
                    await MoveToPermanentStorageAsync(minioFileLocation, cancellationToken);
                    await _redis.RemoveAsync(metadataKey, cancellationToken);
                    await _redis.RemoveAsync(counterKey, cancellationToken);
                }
                else
                {
                    _logger.LogError($"Failed to retrieve metadata for file {minioFileLocation}. Transfer to permanent storage skipped.");
                }
            }

            return minioFileLocation;
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Cannot upload file url: {fileContent.FileName} \nError: {e.Message}");
            throw;
        }
    }

    private async Task MoveToPermanentStorageAsync(string minioFileLocation, CancellationToken cancellationToken)
    {
        await BucketExistAsync(_minioOptions.BucketName, cancellationToken);

        var getArgs = new GetObjectArgs()
            .WithBucket(_minioOptions.TempBucketName)
            .WithObject(minioFileLocation)
            .WithCallbackStream(async (stream, ct) =>
            {
                var putArgs = new PutObjectArgs()
                    .WithBucket(_minioOptions.BucketName)
                    .WithObject(minioFileLocation)
                    .WithStreamData(stream)
                    .WithObjectSize(stream.Length);

                await _minioClient.PutObjectAsync(putArgs, ct);
            });

        await _minioClient.GetObjectAsync(getArgs, cancellationToken);

        await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(_minioOptions.TempBucketName)
            .WithObject(minioFileLocation), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<string> GetFileUrlAsync(
        string key,
        string? bucket = default,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await BucketExistAsync(bucket ?? _minioOptions.BucketName, cancellationToken);
            
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket(bucket ?? _minioOptions.BucketName)
                .WithObject(key)
                .WithExpiry(604000);
            
            return await _minioClient
                .PresignedGetObjectAsync(presignedGetObjectArgs)
                .ConfigureAwait(false);
        }
        catch (MinioException e)
        {
            _logger.LogCritical($"Cannot get file url: {key} \nError: {e.Message}");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task DeleteAsync(
        string key,
        string? bucket = default,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));

        try
        {
            await BucketExistAsync(bucket ?? _minioOptions.BucketName, cancellationToken);
            
            var args = new RemoveObjectArgs()
                .WithBucket(bucket ?? _minioOptions.BucketName)
                .WithObject(key);

            await _minioClient.RemoveObjectAsync(args, cancellationToken).ConfigureAwait(false);
        }
        catch (MinioException e)
        {
            _logger.LogCritical(e.ServerMessage);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<FileContent?> GetFileAsync(
        string key,
        string? bucket = default,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentNullException(nameof(key));
    
        try
        {
            await BucketExistAsync(bucket ?? _minioOptions.BucketName, cancellationToken);
            
            var downloadStream = new MemoryStream();
            var args = new GetObjectArgs()
                .WithBucket(_minioOptions.BucketName)
                .WithObject(key)
                .WithCallbackStream(x =>
                {
                    x.CopyTo(downloadStream);
                    downloadStream.Seek(0, SeekOrigin.Begin);
                });

            var stat = await _minioClient.GetObjectAsync(args, cancellationToken);
            return new FileContent(
                content: downloadStream,
                fileName: stat.ObjectName,
                contentType: stat.ContentType ?? DefaultContentType,
                bucket: _minioOptions.BucketName);
        }
        catch (MinioException e)
        {
            _logger.LogCritical(e.ServerMessage);
            throw;
        }
    }
    
    private async Task BucketExistAsync(string bucket, CancellationToken cancellationToken)
    {
        var beArgs = new BucketExistsArgs()
            .WithBucket(bucket);
        
        var isBucketFound = await _minioClient
            .BucketExistsAsync(beArgs, cancellationToken)
            .ConfigureAwait(false);

        if (isBucketFound)
            return;
        
        var mbArgs = new MakeBucketArgs()
            .WithBucket(bucket);
        
        await _minioClient
            .MakeBucketAsync(mbArgs, cancellationToken).ConfigureAwait(false);
    }

    private static string ContentKey(string fileName)
        => $"{Guid.NewGuid()}/{fileName}";
}