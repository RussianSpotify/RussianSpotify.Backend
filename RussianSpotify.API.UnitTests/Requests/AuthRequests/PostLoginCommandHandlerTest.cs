using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Requests.Auth.PostLogin;
using RussianSpotify.Contracts.Requests.Auth.PostLogin;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.AuthRequests;

/// <summary>
/// Тест для <see cref="PostLoginCommandHandler"/>
/// </summary>
public class PostLoginCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public PostLoginCommandHandlerTest()
    {
        User.Email = "test@mail.ru";
        User.IsConfirmed = true;
        
        _dbContext = CreateInMemory(x => x.AddRange(User));
    }

    /// <summary>
    /// Обработчик должен дать доступ в систему
    /// </summary>
    [Fact]
    public async Task Handle_ShouldLoginUser()
    {
        var request = new PostLoginRequest
        {
            Email = User.Email,
            Password = "12312313",
        };


        var command = new PostLoginCommand(request);
        var handler = new PostLoginCommandHandler(
            TokenFactory.Object,
            _dbContext,
            EmailSender.Object,
            PasswordService.Object,
            UserClaimsManager.Object,
            JwtGenerator.Object,
            Cache.Object);

        var response = await handler.Handle(command, default);

        Assert.NotNull(response);
        Assert.NotNull(response.AccessToken);
        Assert.NotNull(response.RefreshToken);
    }
}