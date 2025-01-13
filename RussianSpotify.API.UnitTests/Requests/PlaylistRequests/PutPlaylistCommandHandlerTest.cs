#region

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Playlist.PutPlaylist;
using RussianSpotify.Contracts.Requests.Playlist.PutPlaylist;
using Xunit;

#endregion

namespace RussianSpotify.API.UnitTests.Requests.PlaylistRequests;

/// <summary>
///     Тест для <see cref="PutPlaylistCommandHandler" />
/// </summary>
public class PutPlaylistCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Playlist _playlist;
    private readonly Song _song;
    private readonly Mock<ILogger<PutPlaylistCommandHandler>> _logger;

    public PutPlaylistCommandHandlerTest()
    {
        _song = Song.CreateForTest(songName: "my song");

        _playlist = Playlist.CreateForTest(
            playlistName: "my playlist",
            author: User.CreateForTest(id: UserContext.Object.CurrentUserId),
            songs: new List<Song>
            {
                _song
            });

        _logger = ConfigureLogger<PutPlaylistCommandHandler>();

        _dbContext = CreateInMemory(
            x => x.AddRange(
                _playlist,
                _song));
    }

    /// <summary>
    ///     Обработчик должен обновить плейлист
    /// </summary>
    [Fact]
    public async Task Handle_ShouldUpdateEntity()
    {
        var request = new PutPlaylistRequest
        {
            PlaylistName = "my playlist #2",
            ImageId = null,
            SongsIds = new List<Guid>(),
        };

        var command = new PutPlaylistCommand(request, _playlist.Id);

        var handler = new PutPlaylistCommandHandler(
            _dbContext,
            UserContext.Object,
            _logger.Object,
            S3Service.Object);

        var response = await handler.Handle(command, default);

        var entity = await _dbContext.Playlists
            .FirstOrDefaultAsync(x => x.Id == response.PlaylistId);

        Assert.NotNull(entity);
        Assert.Equal(request.PlaylistName, entity.PlaylistName);
        Assert.Empty(entity.Songs!);
    }
}