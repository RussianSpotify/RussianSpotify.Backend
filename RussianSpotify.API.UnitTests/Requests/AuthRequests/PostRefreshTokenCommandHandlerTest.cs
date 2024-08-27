using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Requests.Auth.PostRefreshToken;
using RussianSpotify.Contracts.Requests.Auth.PostRefreshToken;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.AuthRequests;

/// <summary>
/// Тест для <see cref="PostRefreshTokenCommandHandler"/>
/// </summary>
public class PostRefreshTokenCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostRefreshTokenCommandHandlerTest()
    {
        User.RefreshToken = "22222";
        _dbContext = CreateInMemory(x => x.AddRange(User));
    }

    /// <summary>
    /// Обработчик должен создать новую пару токенов
    /// </summary>
    [Fact]
    public async Task Handle_ShouldRefreshToken()
    {
        var request = new PostRefreshTokenRequest
        {
            AccessToken = "11111",
            RefreshToken = "22222"
        };

        var command = new PostRefreshTokenCommand(request);
        var handler = new PostRefreshTokenCommandHandler(
            JwtGenerator.Object,
            _dbContext,
            DateTimeProvider.Object);

        var response = await handler.Handle(command, default);

        Assert.NotNull(response);
        Assert.NotNull(response.AccessToken);
        Assert.NotNull(response.RefreshToken);
    }
}