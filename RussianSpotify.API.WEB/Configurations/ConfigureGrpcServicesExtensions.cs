#region

using Payments;
using RussianSpotify.API.Grpc;
using RussianSpotify.API.Grpc.Clients;
using RussianSpotify.API.Grpc.Clients.FileClient;
using RussianSpotify.API.Grpc.Options;

#endregion

namespace RussianSpotify.API.WEB.Configurations;

/// <summary>
///     Класс, предоставляющий методы расширения для настройки gRPC сервисов.
/// </summary>
public static class ConfigureGrpcServicesExtensions
{
    /// <summary>
    ///     Метод для добавления gRPC-сервисов в контейнер зависимостей.
    /// </summary>
    /// <param name="services">Коллекция сервисов <see cref="IServiceCollection" />.</param>
    /// <param name="grpcOptions">Настройки gRPC-сервисов <see cref="GrpcOptions" />.</param>
    public static void AddGrpcServices(this IServiceCollection services, GrpcOptions grpcOptions)
    {
        var fileServerUri = grpcOptions.GetServerUri("File");
        var paymentServerUri = grpcOptions.GetServerUri("Payment");
        
        services.AddGrpcClientService<FileService.FileServiceClient>(fileServerUri);
        services.AddGrpcClientService<PaymentService.PaymentServiceClient>(paymentServerUri);
        
        services.AddScoped<IFileServiceClient, FileServiceClient>();
    }

    /// <summary>
    ///     Метод получения URI сервера для указанного имени сервиса.
    /// </summary>
    /// <param name="grpcOptions">Настройки gRPC-сервисов <see cref="GrpcOptions" />.</param>
    /// <param name="name">Имя сервиса.</param>
    /// <returns>URI сервера сервиса в виде строки.</returns>
    private static string GetServerUri(this GrpcOptions grpcOptions, string name)
        => grpcOptions.Services.Single(x => x.Name == name).ServerUri;
}