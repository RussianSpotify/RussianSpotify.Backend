using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotify.API.Gql.Gql.Main;

[ExtendObjectType(Name = "Query")]
// ReSharper disable once ClassNeverInstantiated.Global
public class SongQuery
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Получение песен")]
    public IQueryable<Song> GetSongs([Service] IDbContext dbContext)
    {
        return dbContext.Songs;
    }

    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Category> GetCategories([Service] IDbContext dbContext)
    {
        return dbContext.Categories;
    }
}