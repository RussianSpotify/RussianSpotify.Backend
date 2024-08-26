using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.AspNetCore;
using RussianSpotify.API.Core.Abstractions;

namespace RussianSpotify.Data.S3;

/// <summary>
/// Входная точка для S3
/// </summary>
public static class Entry
{
    public static IServiceCollection AddS3Storage(
        this IServiceCollection serviceCollection, MinioOptions options)
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options));

        if (string.IsNullOrEmpty(options.AccessKey))
            throw new ArgumentException(nameof(options.AccessKey));

        if (string.IsNullOrEmpty(options.SecretKey))
            throw new ArgumentException(nameof(options.SecretKey));

        if (string.IsNullOrEmpty(options.BucketName))
            throw new AggregateException(nameof(options.BucketName));

        if (string.IsNullOrEmpty(options.ServiceUrl))
            throw new ArgumentException(nameof(options.ServiceUrl));


        serviceCollection.AddMinio(minioOptions =>
        {
            minioOptions.Endpoint = options.ServiceUrl;
            minioOptions.AccessKey = options.AccessKey;
            minioOptions.SecretKey = options.SecretKey;
            
        });
        
        serviceCollection.AddSingleton(options);
        serviceCollection.AddScoped<IS3Service, S3Service>();

        return serviceCollection;
    }
}