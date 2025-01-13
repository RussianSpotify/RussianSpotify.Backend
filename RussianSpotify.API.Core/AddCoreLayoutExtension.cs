#region

using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RussianSpotify.API.Client;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Common.Behaviors;
using RussianSpotify.API.Core.Services;
using RussianSpotify.API.Core.Services.Filters;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.Shared.Services;

#endregion

namespace RussianSpotify.API.Core;

/// <summary>
///     Добавление Core слоя(Инъекция всех зависимостей Core)
/// </summary>
public static class AddCoreLayoutExtension
{
    /// <summary>
    ///     Добавление сервисов в коллекцию
    /// </summary>
    /// <param name="services">Builder.Services</param>
    /// <returns>Коллекцию сервисов с добавленными зависимостями</returns>
    public static void AddCoreLayout(this IServiceCollection services)
    {
        services.AddMediatR(config
            => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddSignalR();
        services.AddScoped<IJwtGenerator, JwtGenerator>();
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ISubscriptionHandler, SubscriptionHandler>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<IUserClaimsManager, UserClaimsManager>();
        services.AddScoped<IRoleManager, RoleManager>();
        services.AddScoped<IFilterHandler, FilterHandler>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenFactory, TokenFactory>();
        services.AddScoped<IPasswordChanger, PasswordChanger>();
    }

    /// <summary>
    ///     Добавить интеграцию с сервисом Google
    /// </summary>
    /// <param name="services">Сервисы</param>
    /// <param name="options">Настройки</param>
    /// <returns>Сервисы</returns>
    public static void AddGoogleService(this IServiceCollection services, HttpApiClientOptions options)
    {
        services.AddHttpClient<IGoogleClient, GoogleClient>((_, client) =>
        {
            client.BaseAddress = new Uri(options.BaseAddress);
        });

        services.AddScoped<IGoogleService, GoogleService>();
    }
}