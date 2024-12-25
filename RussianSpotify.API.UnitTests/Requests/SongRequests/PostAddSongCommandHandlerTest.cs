using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Music.PostAddSong;
using RussianSpotify.Contracts.Enums;
using RussianSpotify.Contracts.Requests.Music.AddSong;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.SongRequests;

/// <summary>
/// Тест для <see cref="PostAddSongCommandHandler"/>
/// </summary>
public class PostAddSongCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    
    public PostAddSongCommandHandlerTest()
    {
        var category = Category.CreateForTest(
            categoryType: CategoryType.Rap);
        
        _dbContext = CreateInMemory(x => x.AddRange(User, category));
    }

    /// <summary>
    /// Обработчик должен создать песню
    /// </summary>
    [Fact]
    public async Task Handle_ShouldCreateSong()
    {
        var request = new AddSongRequest
        {
            SongName = "test",
            Duration = 100,
            Category = (int)CategoryType.Rap
        };

        var command = new PostAddSongCommand(request);
        var handler = new PostAddSongCommandHandler(
            _dbContext,
            UserContext.Object,
            S3Service.Object);

        var response = await handler.Handle(command, default);

        Assert.NotNull(response);

        var entity = await _dbContext.Songs.FirstOrDefaultAsync(x => x.Id == response.SongId);

        Assert.NotNull(entity);
        Assert.Equal(request.SongName, entity.SongName);
        Assert.Equal(request.Duration, entity.Duration);
        Assert.Equal((CategoryType)request.Category, entity.Category.CategoryName);
    }
}