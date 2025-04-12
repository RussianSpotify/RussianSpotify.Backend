using System.Reflection;
using MassTransit;
using RussianSpotify.API.Shared.Options;

namespace RussianSpotify.Grpc.SubscriptionService.Configures;

public static class MassTransitConfigure
{
    /// <summary>
    /// Masstransit RabbitMq
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="options">Настройки</param>
    public static void AddMassTransitConfigure(this IServiceCollection services, RabbitMqOptions options)
        => services.AddMassTransit(configure =>
        {
            configure.SetKebabCaseEndpointNameFormatter();
            configure.AddConsumers(Assembly.GetExecutingAssembly());
            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                cfg.Host(options.Host);
            });
        });

}