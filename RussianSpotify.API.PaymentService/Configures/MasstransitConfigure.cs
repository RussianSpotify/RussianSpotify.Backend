using System.Reflection;
using MassTransit;
using RussianSpotify.API.Shared.Options;

namespace RussianSpotify.API.PaymentService.Configures;

public static class MasstransitConfigure
{
    /// <summary>
    /// Masstransit RabbitMq
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="options">Настройки</param>
    public static void AddMasstransitRabbitMqService(this IServiceCollection services, RabbitMqOptions options)
        => services.AddMassTransit(configure =>
        {
            configure.AddConsumers(Assembly.GetExecutingAssembly());
            configure.SetKebabCaseEndpointNameFormatter();
            
            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.ConfigureEndpoints(context);
                cfg.Host(options.Host);
            });
        });
}