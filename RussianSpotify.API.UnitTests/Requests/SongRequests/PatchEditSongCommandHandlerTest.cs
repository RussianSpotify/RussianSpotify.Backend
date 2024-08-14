using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Music.PatchEditSong;
using RussianSpotify.Contracts.Enums;
using RussianSpotify.Contracts.Requests.Music.EditSong;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.SongRequests;

/// <summary>
/// Тест для <see cref="PatchEditSongCommandHandler"/>
/// </summary>
public class PatchEditSongCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Song _song;
    
    public PatchEditSongCommandHandlerTest()
    {
        var category = Category.CreateForTest(
            categoryType: CategoryType.HipHop);
        
        _song = Song.CreateForTest(
            songName: "tester1213",
            duration: 100,
            authors: new List<User>()
            {
                User
            },
            category: Category.CreateForTest(categoryType: CategoryType.Rap));
        
        _dbContext = CreateInMemory(x => x.AddRange(_song, category));
    }

    /// <summary>
    /// Обработчик должен обновить сущность
    /// </summary>
    [Fact]
    public async Task Handle_ShouldUpdateEntity()
    {
        var request = new EditSongRequest
        {
            SongId = _song.Id,
            SongName = "111111",
            Category = (int)CategoryType.HipHop,
            Duration = 1000,
            ImageId = null,
            SongFileId = null
        };

        var command = new PatchEditSongCommand(request);

        var handler = new PatchEditSongCommandHandler(
            _dbContext,
            FileHelper.Object,
            UserContext.Object);

        await handler.Handle(command, default);

        Assert.Equal(request.SongId, _song.Id);
        Assert.Equal(request.SongName, _song.SongName);
        Assert.Equal((CategoryType)request.Category, _song.Category.CategoryName);
        Assert.Equal(request.Duration, _song.Duration);
    }
}