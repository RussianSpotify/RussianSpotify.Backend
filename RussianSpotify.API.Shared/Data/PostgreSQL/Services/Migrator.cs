#region

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RussianSpotify.API.Shared.Interfaces;

#endregion

namespace RussianSpotify.API.Shared.Data.PostgreSQL.Services;

public class Migrator<TContext> 
    : IMigrator where TContext
    : DbContext
{
    private readonly TContext _dbContext;

    private readonly ILogger<Migrator<TContext>> _logger;

    public Migrator(ILogger<Migrator<TContext>> logger, TContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var migrateId = Guid.NewGuid().ToString();
            _logger.LogInformation($"Apply migrations started: {migrateId}");
            await _dbContext.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            _logger.LogInformation($"Apply migrations succseffuly {migrateId}");
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Failed apply migrations {e.Message}");
            throw;
        }
    }
}