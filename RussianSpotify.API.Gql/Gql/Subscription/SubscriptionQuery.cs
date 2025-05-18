using RussianSpotify.Grpc.SubscriptionService.Data;

namespace RussianSpotify.API.Gql.Gql.Subscription;

[ExtendObjectType(Name = "Query")]
// ReSharper disable once ClassNeverInstantiated.Global
public class SubscriptionQuery
{
    [UseOffsetPaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("Получение подписок пользователя")]
    public IQueryable<RussianSpotify.Grpc.SubscriptionService.Domain.Entities.Subscription> GetSubscriptions(
        IDbContext dbContext, Guid userId)
    {
        return dbContext.Subscriptions
            .Where(s => s.UserId == userId);
    }
}