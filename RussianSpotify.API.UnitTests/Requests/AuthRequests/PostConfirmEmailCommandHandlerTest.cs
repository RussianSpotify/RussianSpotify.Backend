using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Requests.Auth.PostConfirmEmail;
using RussianSpotify.Contracts.Requests.Auth.PostConfirmEmail;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.AuthRequests;

/// <summary>
/// Тест для <see cref="PostConfirmEmailCommandHandler"/>
/// </summary>
public class PostConfirmEmailCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public PostConfirmEmailCommandHandlerTest()
    {
        _dbContext = CreateInMemory(x => x.AddRange(User));
    }

    /// <summary>
    /// Обработчик должен подтвердить почту
    /// </summary>
    [Fact]
    public async Task Handle_ShouldConfirmEmail()
    {
        var request = new PostConfirmEmailRequest
        {
            Email = User.Email,
            EmailVerificationCodeFromUser = CodeForRedis
        };

        var command = new PostConfirmEmailCommand(request);
        var handler = new PostConfirmEmailCommandHandler(Cache.Object, _dbContext);

        await handler.Handle(command, default);
        
        Assert.True(User.IsConfirmed);
    }
}