using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Domain.Constants;

namespace RussianSpotify.API.Gql.Gql.Main;

[ExtendObjectType(Name = "Query")]
// ReSharper disable once ClassNeverInstantiated.Global
public class AuthorQuery
{
    public IQueryable<User> GetAuthors([Service] IDbContext dbContext)
    {
        return dbContext.Users
            .Where(x => x.Roles.Select(y => y.Id).Contains(Roles.AuthorId));
    }
}