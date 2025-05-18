using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotify.API.Gql.Gql.Main;

[ExtendObjectType(Name = "Query")]
// ReSharper disable once ClassNeverInstantiated.Global
public class PlaylistQuery
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Получение плейлистов")]
    public IQueryable<Playlist> GetPlaylists([Service] IDbContext dbContext)
    {
        return dbContext.Playlists;
    }
}