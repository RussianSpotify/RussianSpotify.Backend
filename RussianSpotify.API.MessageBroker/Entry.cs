using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using RussianSpotify.API.MessageBroker.Consumers;

namespace RussianSpotify.API.MessageBroker;

/// <summary>
/// Входная точка для RabbitMQ
/// </summary>
public static class Entry
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, RabbitMqOptions options)
    {
        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();

            busConfigurator.AddConsumer<CreateMessageConsumer>();
            
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.ConfigureEndpoints(context);
                configurator.Host(options.Host);
            });
        });

        return services;
    }
}