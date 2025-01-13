#region

using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Music.PostAddSongAuthor;
using RussianSpotify.Contracts.Requests.Music.AddSongAuthor;
using Xunit;

#endregion

namespace RussianSpotify.API.UnitTests.Requests.SongRequests;

/// <summary>
///     Тест для <see cref="PostAddSongAuthorCommandHandler" />
/// </summary>
public class PostAddSongAuthorCommandHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly User _user;
    private readonly Song _song;

    public PostAddSongAuthorCommandHandlerTest()
    {
        _user = User.CreateForTest(
            login: "1111",
            email: "email");

        _song = Song.CreateForTest(
            songName: "123",
            authors: new List<User>
            {
                User
            });

        _dbContext = CreateInMemory(
            x => x.AddRange(
                _song,
                _user));
    }

    /// <summary>
    ///     Обработчик должен добавить автора к песни
    /// </summary>
    [Fact]
    public async Task Handle_ShouldAddAuthorInSong()
    {
        var request = new AddSongAuthorRequest
        {
            AuthorEmail = _user.Email!,
            SongId = _song.Id,
        };

        var command = new PostAddSongAuthorCommand(request);
        var handler = new PostAddSongAuthorCommandHandler(
            _dbContext,
            UserContext.Object,
            RoleManager.Object);

        var response = await handler.Handle(command, default);

        var entity = await _dbContext.Songs.FirstOrDefaultAsync(x => x.Id == response.SongId);

        Assert.NotNull(entity);
        Assert.Equal(2, entity.Authors.Count);
    }
}