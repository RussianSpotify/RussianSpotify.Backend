using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;

namespace RussianSpotify.API.Gql.Gql.Main;

[ExtendObjectType(Name = "Query")]
// ReSharper disable once ClassNeverInstantiated.Global
public class AccountQuery
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Получение информации о пользователе")]
    public IQueryable<User> GetUser([Service] IDbContext dbContext, Guid userId)
    {
        return dbContext.Users
            .Where(u => u.Id == userId);
    }
}