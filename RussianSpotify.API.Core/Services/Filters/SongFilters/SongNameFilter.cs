#region

using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;

#endregion

namespace RussianSpotify.API.Core.Services.Filters.SongFilters;

/// <inheritdoc />
public class SongNameFilter : IFilter<Song>
{
    public Task<IOrderedQueryable<Song>> FilterAsync(IQueryable<Song> queryable, string filterValue,
        CancellationToken cancellationToken)
        => Task.FromResult(queryable
            .Where(song => song.SongName.ToLower().Contains(filterValue.ToLower()))
            .OrderByDescending(i => i.PlaysNumber));
}