using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RussianSpotify.API.Shared.Domain.Abstractions;

namespace RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;

/// <summary>
/// Перехватчик для soft удаления
/// </summary>
public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    /// <inheritdoc />
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context == null)
            return await base.SavingChangesAsync(eventData, result, cancellationToken);

        var entries = context.ChangeTracker
            .Entries<ISoftDeletable>()
            .Where(x => x.State == EntityState.Deleted);

        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            entry.Entity.DeletedAt = DateTime.UtcNow;
            entry.Entity.IsDeleted = true;
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}