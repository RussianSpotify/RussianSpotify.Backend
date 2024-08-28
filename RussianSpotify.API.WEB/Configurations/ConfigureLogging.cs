using Serilog;

namespace RussianSpotify.API.WEB.Configurations;

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