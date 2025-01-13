#region

using Microsoft.Extensions.Diagnostics.HealthChecks;
using Minio;
using Minio.AspNetCore;
using Minio.AspNetCore.HealthChecks;
using RussianSpotify.API.Files.Interfaces;
using MinioOptions = RussianSpotify.API.Files.Options.MinioOptions;

#endregion

namespace RussianSpotify.API.Files.Services.S3Service;

/// <summary>
///     Класс для регистрации S3-хранилища в DI-контейнере.
/// </summary>
public static class S3Entry
{
    /// <summary>
    ///     Регистрирует S3-хранилище в DI-контейнере.
    /// </summary>
    /// <param name="serviceCollection">Коллекция сервисов для регистрации.</param>
    /// <param name="options">Настройки для подключения к S3-хранилищу.</param>
    /// <exception cref="ArgumentNullException">
    ///     Выбрасывается, если <paramref name="serviceCollection" /> или <paramref name="options" /> равны <c>null</c>.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     Выбрасывается, если настройки в <paramref name="options" /> содержат недопустимые значения (например, пустые
    ///     строки).
    /// </exception>
    /// <exception cref="AggregateException">
    ///     Выбрасывается, если произошла ошибка при подключении к S3-хранилищу.
    /// </exception>
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

        if (string.IsNullOrEmpty(options.TempBucketName))
            throw new ArgumentException(nameof(options.TempBucketName));

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
                name: $"{options.MinioClient}_MainBucket",
                bucket: options.BucketName,
                failureStatus: HealthStatus.Healthy)
            .AddMinio(
                factory: provider => provider.GetRequiredService<IMinioClient>(),
                name: $"{options.MinioClient}_TempBucket",
                bucket: options.TempBucketName,
                failureStatus: HealthStatus.Healthy);

        serviceCollection.AddSingleton(options);
        serviceCollection.AddScoped<IS3Service, S3Service>();
    }
}