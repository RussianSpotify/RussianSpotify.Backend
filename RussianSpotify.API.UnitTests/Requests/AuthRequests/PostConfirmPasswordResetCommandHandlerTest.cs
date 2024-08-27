using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Requests.Auth.PostConfirmPasswordReset;
using RussianSpotify.Contracts.Requests.Auth.PostConfirmPasswordReset;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.AuthRequests;

/// <summary>
/// Обработчик для <see cref="PostConfirmPasswordResetCommandHandler"/>
/// </summary>
public class PostConfirmPasswordResetCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostConfirmPasswordResetCommandHandlerTest()
    {
        _dbContext = CreateInMemory(x => x.AddRange(User));
    }

    /// <summary>
    /// Должен сменить пароль на новый
    /// </summary>
    [Fact]
    public async Task Handle_ShouldConfirmResetPassword()
    {
        var request = new PostConfirmPasswordResetRequest
        {
            Email = User.Email,
            VerificationCodeFromUser = CodeForRedis,
            NewPassword = "123",
            NewPasswordConfirm = "123"
        };

        var command = new PostConfirmPasswordResetCommand(request);
        var handler = new PostConfirmPasswordResetCommandHandler(
            Cache.Object,
            _dbContext,
            PasswordService.Object);

        await handler.Handle(command, default);
    }
}