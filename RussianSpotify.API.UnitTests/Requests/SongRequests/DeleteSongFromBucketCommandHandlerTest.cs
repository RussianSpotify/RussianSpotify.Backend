#region

using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Music.DeleteSongFromBucket;
using Xunit;

#endregion

namespace RussianSpotify.API.UnitTests.Requests.SongRequests;

/// <summary>
///     Тест для <see cref="DeleteSongFromBucketCommandHandler" />
/// </summary>
public class DeleteSongFromBucketCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Song _song;

    public DeleteSongFromBucketCommandHandlerTest()
    {
        _song = Song.CreateForTest(songName: "test");

        SetUserSongs(new List<Song>
        {
            _song
        });

        _dbContext = CreateInMemory(x => x.AddRange(_song, User));
    }

    /// <summary>
    ///     Обработчик должен удалить песню из корзины пользователя
    /// </summary>
    [Fact]
    public async Task Handle_ShouldRemoveSongFromUserBucket()
    {
        var request = new DeleteSongFromBucketCommand(_song.Id);

        var handler = new DeleteSongFromBucketCommandHandler(_dbContext, UserContext.Object);
        await handler.Handle(request, default);

        Assert.Empty(User.Bucket!.Songs);
    }
}