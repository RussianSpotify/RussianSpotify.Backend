#region

using MassTransit;
using RussianSpotify.API.Shared.Options;

#endregion

namespace RussianSpotify.API.WEB.Configurations;

/// <summary>
///     Конфигурация RabbitMQ
/// </summary>
public static class ConfigureRabbitMq
{
    /// <summary>
    ///     Добавить RabbitMQ
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="options">Конфигурация для RabbitMQ</param>
    public static void AddRabbitMq(this IServiceCollection services, RabbitMqOptions options)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.ConfigureEndpoints(context);
                configurator.Host(options.Host);
            });
        });
    }
}