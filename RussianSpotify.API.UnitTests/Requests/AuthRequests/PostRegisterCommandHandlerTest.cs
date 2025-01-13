#region

using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Auth.PostRegister;
using RussianSpotify.Contracts.Requests.Auth.PostRegister;
using Xunit;

#endregion

namespace RussianSpotify.API.UnitTests.Requests.AuthRequests;

/// <summary>
///     Тест для <see cref="PostRegisterCommandHandler" />
/// </summary>
public class PostRegisterCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Role _role;

    /// <summary>
    ///     Конструктор
    /// </summary>
    public PostRegisterCommandHandlerTest()
    {
        _role = Role.CreateForTest(name: "Автор");
        _dbContext = CreateInMemory(x => x.AddRange(_role));
    }

    /// <summary>
    ///     Обработчик должен создать пользователя
    /// </summary>
    [Fact]
    public async Task Handle_ShouldRegisterUser()
    {
        var request = new PostRegisterRequest
        {
            UserName = "test",
            Password = "11111111",
            PasswordConfirm = "11111111",
            Email = "email@mail.ru",
            Role = _role.Name,
        };

        var command = new PostRegisterCommand(request);
        var handler = new PostRegisterCommandHandler(
            _dbContext,
            PasswordService.Object,
            TokenFactory.Object,
            EmailSender.Object,
            Cache.Object);

        var response = await handler.Handle(command, default);

        Assert.NotNull(response);
        Assert.NotNull(response.Email);

        var entity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        Assert.NotNull(entity);

        var entityRole = Assert.Single(entity.Roles);

        Assert.Equal(request.Email, entity.Email);
        Assert.Equal(request.UserName, entity.UserName);
        Assert.Equal(request.Role, entityRole.Name);
    }
}