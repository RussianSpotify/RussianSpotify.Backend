using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Playlist.PostAddPlaylistToFavourite;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.Playlist;

/// <summary>
/// Тест для <see cref="PostAddPlaylistToFavouriteCommandHandler"/>
/// </summary>
public class PostAddPlaylistToFavouriteCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Core.Entities.Playlist _playlist;
    
    public PostAddPlaylistToFavouriteCommandHandlerTest()
    {
        _playlist = Core.Entities.Playlist.CreateForTest(
            playlistName: "test");

        var user = User.CreateForTest(id: UserContext.Object.CurrentUserId);

        _dbContext = CreateInMemory(x => x.AddRange(_playlist, user));
    }

    /// <summary>
    /// Обработчик должен добавить плейлист в любимые
    /// </summary>
    [Fact]
    public async Task Handle_ShouldAddPlaylistInFavourite()
    {
        var request = new PostAddPlaylistToFavouriteCommand(_playlist.Id);

        var handler = new PostAddPlaylistToFavouriteCommandHandler(_dbContext, UserContext.Object);
        await handler.Handle(request, default);

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == UserContext.Object.CurrentUserId);
        
        Assert.NotNull(user);

        Assert.NotNull(user.Playlists);
        Assert.NotEmpty(user.Playlists);
        
        var playlist = Assert.Single(user.Playlists);
        
        Assert.Equal(_playlist.Id, playlist.Id);
        Assert.Equal(_playlist.PlaylistName, playlist.PlaylistName);
    }
}