namespace RussianSpotify.API.Shared.Interfaces;

/// <summary>
///     Background-служба
/// </summary>
public interface IWorker
{
    /// <summary>
    ///     Запустить службу
    /// </summary>
    /// <returns></returns>
    Task RunAsync();
}