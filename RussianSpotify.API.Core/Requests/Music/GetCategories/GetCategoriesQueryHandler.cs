using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Extensions;
using RussianSpotify.Contracts.Requests.Music.GetCategories;

namespace RussianSpotify.API.Core.Requests.Music.GetCategories;

/// <summary>
/// Обработчик для <see cref="GetCategoriesQuery"/>
/// </summary>
public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, GetCategoriesResponse>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public GetCategoriesQueryHandler(IDbContext dbContext)
        => _dbContext = dbContext;

    /// <inheritdoc/>
    public async Task<GetCategoriesResponse> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        
        var result = await _dbContext.Categories
            .Select(x => new GetCategoriesResponseItem
            {
                CategoryNumber = (int)x.CategoryName,
                CategoryName = x.CategoryName.GetDescription(),
            })
            .ToListAsync(cancellationToken);

        return new GetCategoriesResponse(result);
    }
}