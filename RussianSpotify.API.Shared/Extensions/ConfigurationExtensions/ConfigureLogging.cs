using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace RussianSpotify.API.Shared.Extensions.ConfigurationExtensions;

/// <summary>
/// Добавление логирования (Serilog)
/// </summary>
public static class ConfigureLogging
{
    /// <summary>
    /// Добавить логирование
    /// </summary>
    /// <param name="serviceCollection">Сервисы</param>
    public static void AddCustomLogging(this IServiceCollection serviceCollection)
        => serviceCollection.AddLogging(
            logging => logging.AddSerilog(new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .CreateLogger()));
}