#region

using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Requests.Auth.PostRevokeToken;
using RussianSpotify.Contracts.Requests.Auth.PostRevokeToken;
using Xunit;

#endregion

namespace RussianSpotify.API.UnitTests.Requests.AuthRequests;

/// <summary>
///     Тест для <see cref="PostRevokeTokenCommandHandler" />
/// </summary>
public class PostRevokeTokenCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;

    /// <summary>
    ///     Конструктор
    /// </summary>
    public PostRevokeTokenCommandHandlerTest()
    {
        User.RefreshToken = "хуй";
        _dbContext = CreateInMemory(x => x.AddRange(User));
    }

    /// <summary>
    ///     Обработчик должен снести токен обновления
    /// </summary>
    [Fact]
    public async Task Handle_ShouldRevokeRefreshToken()
    {
        var request = new PostRevokeTokenRequest
        {
            Email = User.Email,
        };

        var command = new PostRevokeTokenCommand(request);
        var handler = new PostRevokeTokenCommandHandler(_dbContext, UserContext.Object);

        await handler.Handle(command, default);

        Assert.Null(User.RefreshToken);
    }
}