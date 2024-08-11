using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Author.GetAuthor;
using RussianSpotify.Contracts.Requests.Author.GetAuthor;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.Author;

/// <summary>
/// Тест для <see cref="GetAuthorQueryHandler"/>
/// </summary>
public class GetAuthorQueryHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly User _author;

    public GetAuthorQueryHandlerTest()
    {
        _author = User.CreateForTest(login: "login");
        
        _dbContext = CreateInMemory(x => x.AddRange(_author));
    }

    /// <summary>
    /// Обработчик должен вернуть автора по запросу 
    /// </summary>
    [Fact]
    public async Task Handle_ShouldReturnAuthor()
    {
        var request = new GetAuthorRequest
        {
            Name = "login"
        };

        var command = new GetAuthorQuery(request);
        var handler = new GetAuthorQueryHandler(_dbContext, RoleManager.Object);

        var response = await handler.Handle(command, default);

        Assert.NotNull(response);
        Assert.Equal(_author.UserName, response.Name);
    }
}