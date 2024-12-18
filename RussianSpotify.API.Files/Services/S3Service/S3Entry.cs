using Microsoft.Extensions.Diagnostics.HealthChecks;
using Minio;
using Minio.AspNetCore;
using Minio.AspNetCore.HealthChecks;
using RussianSpotify.API.Files.Interfaces;
using MinioOptions = RussianSpotify.API.Files.Options.MinioOptions;

namespace RussianSpotify.API.Files.Services.S3Service;

public static class S3Entry
{
    public static void AddS3Storage(
        this IServiceCollection serviceCollection,
        MinioOptions options)
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
        
        serviceCollection.AddMinio(name: options.MinioClient, minioOptions =>
        {
            minioOptions.Endpoint = options.ServiceUrl;
            minioOptions.ConfigureClient(client =>
            {
                client.WithSSL(false);
                client.WithEndpoint(options.ServiceUrl);
                client.WithCredentials(options.AccessKey, options.SecretKey);
            });
        });

        serviceCollection.AddHealthChecks()
            .AddCheck<MinioHealthCheck>(
                name: nameof(MinioHealthCheck),
                HealthStatus.Healthy,
                new[] { "external", "storage" })
            .AddMinio(
                factory: provider => provider.GetRequiredService<IMinioClient>(),
                name: options.MinioClient,
                bucket: options.BucketName,
                failureStatus: HealthStatus.Healthy);
        
        serviceCollection.AddSingleton(options);
        serviceCollection.AddScoped<IS3Service, S3Service>();
    }   
}