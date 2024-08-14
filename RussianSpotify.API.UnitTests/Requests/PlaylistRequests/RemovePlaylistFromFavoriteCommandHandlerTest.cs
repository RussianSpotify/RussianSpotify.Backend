using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Playlist.RemovePlaylistFromFavorite;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.PlaylistRequests;

/// <summary>
/// Тест для <see cref="RemovePlaylistFromFavoriteCommandHandler"/>
/// </summary>
public class RemovePlaylistFromFavoriteCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Core.Entities.Playlist _playlist;
    
    public RemovePlaylistFromFavoriteCommandHandlerTest()
    {
        _playlist = Core.Entities.Playlist.CreateForTest(
            playlistName: "123",
            author: User.CreateForTest(id: UserContext.Object.CurrentUserId));

        _dbContext = CreateInMemory(
            x => x.AddRange(
                _playlist));
    }

    /// <summary>
    /// Обработчик должен удалить плейлист из любимых
    /// </summary>
    [Fact]
    public async Task Handle_ShouldRemovePlaylistFromFavourite()
    {
        var request = new RemovePlaylistFromFavoriteCommand(_playlist.Id);

        var handler = new RemovePlaylistFromFavoriteCommandHandler(_dbContext, UserContext.Object);

        await handler.Handle(request, default);

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == UserContext.Object.CurrentUserId);

        Assert.NotNull(user);
        Assert.NotNull(user.Playlists);
        Assert.Empty(user.Playlists);
    }
}