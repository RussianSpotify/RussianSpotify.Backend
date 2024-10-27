using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Chat.GetStory;
using RussianSpotify.Contracts.Models;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.ChatRequests;

/// <summary>
/// Тест для <see cref="GetStoryQueryHandler"/>
/// </summary>
public class GetStoryQueryHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Chat _chat;
    private readonly Message _message;

    public GetStoryQueryHandlerTest()
    {
        _message = Message.CreateForTest(messageText: "hui", user: User);
        
        _chat = Chat.CreateForTest(
            name: "Test chat",
            users: new List<User>()
            {
                User
            },
            messages: new List<Message>()
            {
                _message
            });

        _dbContext = CreateInMemory(x => x.Add(_chat));
    }

    /// <summary>
    /// Должен вернуть сообщения из чата
    /// </summary>
    [Fact]
    public async Task Handle_ShouldReturnMessagesInChat()
    {
        var request = new GetStoryQuery(_chat.Id);

        var handler = new GetStoryQueryHandler(UserContext.Object, _dbContext);

        var response = await handler.Handle(request, default);

        Assert.NotNull(response);
        Assert.NotNull(response.Entities);
        Assert.Equal(1, response.TotalCount);

        var entity = Assert.Single(response.Entities);

        Assert.NotNull(entity);
        Assert.Equal(_message.Id, entity.Id);
        Assert.Equal(User.Id, entity.SenderId);
    }
}