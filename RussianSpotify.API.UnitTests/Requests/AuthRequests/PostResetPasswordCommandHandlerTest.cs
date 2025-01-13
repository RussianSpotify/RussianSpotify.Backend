#region

using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Requests.Auth.PostResetPassword;
using RussianSpotify.Contracts.Requests.Auth.PostResetPassword;
using Xunit;

#endregion

namespace RussianSpotify.API.UnitTests.Requests.AuthRequests;

/// <summary>
///     Тест для <see cref="PostResetPasswordCommandHandler" />
/// </summary>
public class PostResetPasswordCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;

    /// <summary>
    ///     Конструктор
    /// </summary>
    public PostResetPasswordCommandHandlerTest()
    {
        _dbContext = CreateInMemory(x => x.AddRange(User));
    }

    /// <summary>
    ///     Обработчик должен отправить код об сбросе пароля
    /// </summary>
    [Fact]
    public async Task Handle_ShouldResetPassword()
    {
        var request = new PostResetPasswordRequest
        {
            Email = User.Email,
        };

        var command = new PostResetPasswordCommand(request);
        var handler = new PostResetPasswordCommandHandler(
            _dbContext,
            EmailSender.Object,
            Cache.Object,
            TokenFactory.Object);

        await handler.Handle(command, default);
    }
}