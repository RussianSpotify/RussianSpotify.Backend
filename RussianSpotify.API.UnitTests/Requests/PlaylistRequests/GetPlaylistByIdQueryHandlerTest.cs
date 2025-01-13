#region

using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Playlist.GetPlaylistById;
using Xunit;

#endregion

namespace RussianSpotify.API.UnitTests.Requests.PlaylistRequests;

/// <summary>
///     Тест для <see cref="GetPlaylistByIdQueryHandler" />
/// </summary>
public class GetPlaylistByIdQueryHandlerTest : UnitTestBase
{
    private readonly Playlist _playlist;
    private readonly IDbContext _dbContext;

    public GetPlaylistByIdQueryHandlerTest()
    {
        _playlist = Playlist.CreateForTest(
            playlistName: "123",
            releaseDate: new DateTime(2003, 12, 1),
            author: User.CreateForTest(login: "login"));

        _dbContext = CreateInMemory(x => x.AddRange(_playlist));
    }

    /// <summary>
    ///     Обработчик должен вернуть плейлист по ИД
    /// </summary>
    [Fact]
    public async Task Handle_WithId_ShouldReturnPlaylist()
    {
        var request = new GetPlaylistByIdQuery(_playlist.Id);

        var handler = new GetPlaylistByIdQueryHandler(_dbContext, UserContext.Object);
        var response = await handler.Handle(request, default);

        Assert.NotNull(response);
        Assert.Equal(_playlist.Id, response.Id);
        Assert.Equal(_playlist.PlaylistName, response.PlaylistName);
        Assert.Equal(_playlist.ReleaseDate, response.ReleaseDate);
    }
}