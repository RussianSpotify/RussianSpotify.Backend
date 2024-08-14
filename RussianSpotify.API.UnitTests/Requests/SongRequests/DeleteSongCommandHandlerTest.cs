using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Music.DeleteSong;
using RussianSpotify.Contracts.Requests.Music.DeleteSong;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.SongRequests;

/// <summary>
/// Тест для <see cref="DeleteSongCommandHandler"/>
/// </summary>
public class DeleteSongCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Song _song;

    public DeleteSongCommandHandlerTest()
    {
        _song = Song.CreateForTest(
            songName: "test",
            authors: new List<User>
            {
                User
            });
        
        _dbContext = CreateInMemory(x => x.AddRange(_song));
    }

    /// <summary>
    /// Обработчик должен удалить песню
    /// </summary>
    [Fact]
    public async Task Handle_ShouldDeleteSongAsync()
    {
        var request = new DeleteSongRequest
        {
            SongId = _song.Id
        };

        var command = new DeleteSongCommand(request);
        var handler = new DeleteSongCommandHandler(
            _dbContext,
            UserContext.Object,
            FileHelper.Object);

        await handler.Handle(command, default);
        
        Assert.Equal(0, _dbContext.Songs.Count());
    }
}