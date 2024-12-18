using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Playlist.PostCreatePlaylist;
using RussianSpotify.Contracts.Enums;
using RussianSpotify.Contracts.Requests.Playlist.PostCreatePlaylist;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.PlaylistRequests;

/// <summary>
/// Тест для <see cref="PostCreatePlaylistCommandHandler"/>
/// </summary>
public class PostCreatePlaylistCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Guid _image = Guid.NewGuid();
    private readonly User _user;
    private readonly Song _song;

    public PostCreatePlaylistCommandHandlerTest()
    {
        _user = User.CreateForTest(id: UserContext.Object.CurrentUserId);

        _song = Song.CreateForTest(
            songName: "test",
            category: Category.CreateForTest(categoryType: CategoryType.Rap));
        
        _dbContext = CreateInMemory(
            x => x.AddRange(
                _user,
                _song));
    }

    /// <summary>
    /// Обработчик должен создать плейлист
    /// </summary>
    [Fact]
    public async Task Handle_ShouldCreatePlaylist()
    {
        var request = new PostCreatePlaylistRequest
        {
            PlaylistName = "Тест",
            ImageId = _image,
            SongIds = new List<Guid>() { _song.Id },
            IsAlbum = false
        };

        var command = new PostCreatePlaylistCommand(request);
        var handler = new PostCreatePlaylistCommandHandler(
            _dbContext,
            UserContext.Object,
            DateTimeProvider.Object,
            S3Service.Object);

        var response = await handler.Handle(command, default);

        var playlist = await _dbContext.Playlists.FirstOrDefaultAsync(x => x.Id == response.PlaylistId);

        Assert.NotNull(playlist);
        
        Assert.Equal(_image, playlist.ImageFileId);
        Assert.Equal(request.PlaylistName, playlist.PlaylistName);
        Assert.Equal(request.IsAlbum, playlist.IsAlbum);
        Assert.Equal(_song.Id, playlist.Songs![0].Id);
        Assert.Equal(_user.Id, playlist.AuthorId);
        Assert.Equal(DateTimeProvider.Object.CurrentDate, playlist.ReleaseDate);
        Assert.Single(playlist.Users!);
    }
}