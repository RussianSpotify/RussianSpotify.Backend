using RussianSpotify.API.Grpc;
using RussianSpotify.API.Grpc.Clients;
using RussianSpotify.API.Grpc.Clients.FileClient;
using RussianSpotify.API.Grpc.Options;

namespace RussianSpotify.API.WEB.Configurations;

public static class ConfigureGrpcServicesExtensions
{
    public static void AddGrpcServices(this IServiceCollection services, GrpcOptions grpcOptions)
    {
        var fileServerUri = grpcOptions.GetServerUri("File");
        services.AddGrpcClientService<FileService.FileServiceClient>(fileServerUri);
        services.AddScoped<IFileServiceClient, FileServiceClient>();
    }
    
    private static string GetServerUri(this GrpcOptions grpcOptions, string name)
        => grpcOptions.Services.Single(x => x.Name == name).ServerUri;
}