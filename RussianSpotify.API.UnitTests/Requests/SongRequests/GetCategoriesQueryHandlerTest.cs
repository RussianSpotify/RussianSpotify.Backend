using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Extensions;
using RussianSpotify.API.Core.Requests.Music.GetCategories;
using RussianSpotify.Contracts.Enums;
using Xunit;

namespace RussianSpotify.API.UnitTests.Requests.SongRequests;

/// <summary>
/// Тест для <see cref="GetCategoriesQueryHandler"/>
/// </summary>
public class GetCategoriesQueryHandlerTest : UnitTestBase
{
    private readonly IDbContext _dbContext;
    private readonly Category _category;

    /// <summary>
    /// Конструктор
    /// </summary>
    public GetCategoriesQueryHandlerTest()
    {
        _category = Category.CreateForTest(
            categoryType: CategoryType.Rap);
        
        _dbContext = CreateInMemory(x => x.AddRange(_category));
    }
    
    /// <summary>
    /// Обработчик должен вернуть категории
    /// </summary>
    [Fact]
    public async Task Handle_ShouldReturnCategories()
    {
        var handler = new GetCategoriesQueryHandler(_dbContext);
        var response = await handler.Handle(new GetCategoriesQuery(), default);

        Assert.NotNull(response);
        Assert.NotEmpty(response.Entities);

        var entity = Assert.Single(response.Entities);
        
        Assert.Equal((int)_category.CategoryName, entity.CategoryNumber);
        Assert.Equal(_category.CategoryName.GetDescription(), entity.CategoryName);
    }
}