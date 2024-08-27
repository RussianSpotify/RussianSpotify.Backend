using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Requests.Account.GetUserInfo;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.AccountRequests;

/// <summary>
/// Тест для <see cref="GetUserInfoQueryHandler"/>
/// </summary>
public class GetUserInfoQueryHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetUserInfoQueryHandlerTest()
    {
        _dbContext = CreateInMemory(x => x.Add(User));
    }

    /// <summary>
    /// Обработчик должен вернуть информацию о сущности
    /// </summary>
    [Fact]
    public async Task Handle_ShouldReturnEntityInfo()
    {
        var request = new GetUserInfoQuery();
        var handler = new GetUserInfoQueryHandler(UserContext.Object, _dbContext);

        var response = await handler.Handle(request, default);

        Assert.NotNull(response);
        Assert.Equal(User.Id, response.UserId);
        Assert.Equal(User.Email, response.Email);
        Assert.Equal(User.UserName, response.UserName);
        Assert.Equal(User.UserPhotoId, response.UserPhotoId);
    }
}