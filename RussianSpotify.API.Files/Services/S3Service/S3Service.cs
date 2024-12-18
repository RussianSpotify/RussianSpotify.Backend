using Minio;
using Minio.AspNetCore;
using Minio.DataModel.Args;
using Minio.Exceptions;
using RussianSpotify.API.Files.Interfaces;
using RussianSpotify.API.Files.Models;

namespace RussianSpotify.API.Files.Services.S3Service;

public class S3Service: IS3Service
{
    private const string DefaultContentType = "application/octet-stream";

    private readonly Options.MinioOptions _minioOptions;
    private readonly ILogger<S3Service> _logger;
    private readonly IMinioClient _minioClient;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="minioOptions">Настройки для S3</param>
    /// <param name="logger">Логгер</param>
    /// <param name="minioClient">Клиент Minio</param>
    public S3Service(
        Options.MinioOptions minioOptions,
        ILogger<S3Service> logger,
        IMinioClient minioClient)
    {
        _minioOptions = minioOptions;
        _logger = logger;
        _minioClient = minioClient;
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

            await BucketExistAsync(_minioOptions.BucketName, cancellationToken);
        
            var putArgs = new PutObjectArgs()
                .WithBucket(_minioOptions.BucketName)
                .WithObject(minioFileLocation)
                .WithStreamData(fileContent.Content)
                .WithObjectSize(fileContent.Content.Length);

            await _minioClient.PutObjectAsync(putArgs, cancellationToken);
            return minioFileLocation;
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Cannot upload file url: {fileContent.FileName} \nError: {e.Message}");
            throw;
        }
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
            .WithBucket(_minioOptions.BucketName);
        
        await _minioClient
            .MakeBucketAsync(mbArgs, cancellationToken).ConfigureAwait(false);
    }

    private string ContentKey(string? fileName)
        => $"{DateTime.UtcNow.Date:dd:MM:yyyy}/{Guid.NewGuid()}/{fileName}";
}