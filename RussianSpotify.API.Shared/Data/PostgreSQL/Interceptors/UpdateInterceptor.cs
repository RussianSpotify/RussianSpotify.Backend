using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RussianSpotify.API.Shared.Domain.Abstractions;

namespace RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;

/// <summary>
/// Перехватчик для обновления
/// </summary>
public class UpdateInterceptor : SaveChangesInterceptor
{
    /// <inheritdoc />
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = eventData.Context.ChangeTracker
            .Entries<ITimeTrackable>()
            .Where(x => x.State is EntityState.Modified or EntityState.Added);

        foreach (var entry in entries)
        {
            if (entry.Entity.CreatedAt == default)
                entry.Entity.CreatedAt = DateTime.UtcNow;

            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}