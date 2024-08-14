using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Music.PostAddSongToFavourite;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.SongRequests;

/// <summary>
/// Тест для <see cref="PostAddSongToFavouriteCommandHandler"/>
/// </summary>
public class PostAddSongToFavouriteCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Song _song;
    
    public PostAddSongToFavouriteCommandHandlerTest()
    {
        _song = Song.CreateForTest(songName: "1111");

        _dbContext = CreateInMemory(x => x.AddRange(User, _song));
    }

    /// <summary>
    /// Обработчик должен добавить песню в корзину
    /// </summary>
    [Fact]
    public async Task Handle_ShouldAddSongToFavourite()
    {
        var request = new PostAddSongToFavouriteCommand(_song.Id);

        var handler = new PostAddSongToFavouriteCommandHandler(_dbContext, UserContext.Object);
        await handler.Handle(request, default);
        
        var song = Assert.Single(User.Bucket!.Songs);
        
        Assert.Equal(_song.Id, song.Id);
        Assert.Equal(_song.SongName, song.SongName);
    }
}