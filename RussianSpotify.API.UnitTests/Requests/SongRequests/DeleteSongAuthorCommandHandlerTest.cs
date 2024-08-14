using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Music.DeleteSongAuthor;
using RussianSpotify.Contracts.Requests.Music.DeleteSongAuthor;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.SongRequests;

/// <summary>
/// Тест для <see cref="DeleteSongAuthorCommandHandler"/>
/// </summary>
public class DeleteSongAuthorCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Song _song;
    private readonly User _user;
    
    public DeleteSongAuthorCommandHandlerTest()
    {
        _user = User.CreateForTest(login: "author");
        
        _song = Song.CreateForTest(
            songName: "13",
            authors: new List<User>()
            {
                User,
                _user
            });
        
        _dbContext = CreateInMemory(x => x.AddRange(_song));
    }

    /// <summary>
    /// Обработчик должен удалить автора из песни
    /// </summary>
    [Fact]
    public async Task Handle_ShouldRemoveAuthorFromSong()
    {
        var request = new DeleteSongAuthorRequest
        {
            SongId = _song.Id,
            AuthorId = _user.Id,
        };

        var command = new DeleteSongAuthorCommand(request);

        var handler = new DeleteSongAuthorCommandHandler(_dbContext, UserContext.Object);
        await handler.Handle(command, default);
        
        Assert.Single(_song.Authors);
    }
}