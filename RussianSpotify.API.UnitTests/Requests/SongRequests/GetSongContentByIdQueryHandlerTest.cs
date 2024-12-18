using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Music.GetSongContentById;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.SongRequests;

/// <summary>
/// Тест для <see cref="GetSongContentByIdQueryHandler"/>
/// </summary>
public class GetSongContentByIdQueryHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Song _song;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public GetSongContentByIdQueryHandlerTest()
    {
        _song = Song.CreateForTest(
            songName: "test");
        
        _dbContext = CreateInMemory(x => x.AddRange(_song));
    }

    /// <summary>
    /// Обработчик должен отдать контент песни по ид
    /// </summary>
    [Fact]
    public async Task Handle_ShouldGetContentSongById()
    {
        var request = new GetSongContentByIdQuery(_song.Id);
        var handler = new GetSongContentByIdQueryHandler(_dbContext, S3Service.Object);

        var response = await handler.Handle(request, default);

        Assert.NotNull(response);
    }
}