using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Playlist.DeletePlaylist;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.PlaylistRequests;

/// <summary>
/// Тест для <see cref="DeletePlaylistCommandHandler"/>
/// </summary>
public class DeletePlaylistCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Core.Entities.Playlist _playlist;
    
    public DeletePlaylistCommandHandlerTest()
    {
        _playlist = Core.Entities.Playlist
            .CreateForTest(
                playlistName: "test",
                author: User.CreateForTest(id: UserContext.Object.CurrentUserId));
        
        _dbContext = CreateInMemory(x => x.AddRange(_playlist));
    }

    /// <summary>
    /// Обработчик должен удалить плейлист
    /// </summary>
    [Fact]
    public async Task Handle_ShouldDeletePlaylist()
    {
        var command = new DeletePlaylistCommand(_playlist.Id);

        var handler = new DeletePlaylistCommandHandler(
            _dbContext,
            UserContext.Object,
            S3Service.Object);

        var response = await handler.Handle(command, default);

        var isExist = _dbContext.Playlists.Any(x => x.Id == response.PlaylistId);
        
        Assert.False(isExist);
    }
}