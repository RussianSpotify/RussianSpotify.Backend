#region

using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Chat.GetChats;
using RussianSpotify.API.Shared.Domain.Constants;
using Xunit;

#endregion

namespace RussianSpotify.API.UnitTests.Requests.ChatRequests;

/// <summary>
///     Тест для <see cref="GetChatsQueryHandler" />
/// </summary>
public class GetChatsQueryHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Chat _chat;
    private readonly User _user;
    private readonly Message _message;

    public GetChatsQueryHandlerTest()
    {
        _user = User.CreateForTest(
            login: "Tester123",
            roles: new List<Role>
            {
                new()
                {
                    Name = Roles.AuthorRoleName
                }
            });
        _message = Message.CreateForTest(messageText: "123");

        _chat = Chat.CreateForTest(
            name: "Test chat",
            messages: new List<Message>
            {
                _message
            },
            users: new List<User>
            {
                User, _user
            });

        _dbContext = CreateInMemory(x => x.Add(_chat));
    }

    /// <summary>
    ///     Обработчик должен вернуть информацию для чата
    /// </summary>
    [Fact]
    public async Task Handle_ShouldReturnChatInfo()
    {
        var request = new GetChatsQuery();

        var handler = new GetChatsQueryHandler(UserContext.Object, _dbContext);

        var response = await handler.Handle(request, default);

        Assert.NotNull(response);
        Assert.NotNull(response.Entities);
        Assert.NotEmpty(response.Entities);

        Assert.Equal(1, response.TotalCount);
        var entity = Assert.Single(response.Entities);

        Assert.NotNull(entity);
        Assert.Equal(_chat.Id, entity.Id);
        Assert.Equal(_user.UserName, entity.ChatName);
        Assert.Equal(_message.MessageText, entity.LastMessage);
    }
}