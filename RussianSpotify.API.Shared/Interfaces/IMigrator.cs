namespace RussianSpotify.API.Shared.Interfaces;

public interface IMigrator
{
    Task MigrateAsync(CancellationToken cancellationToken = default);
}