using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Extensions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Chat.GetStory;

namespace RussianSpotify.API.Core.Requests.Chat.GetStory;

/// <summary>
/// Обработчик для <see cref="GetStoryQuery"/>
/// </summary>
public class GetStoryQueryHandler : IRequestHandler<GetStoryQuery, GetStoryResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="dbContext">Контекст БД</param>
    public GetStoryQueryHandler(IUserContext userContext, IDbContext dbContext)
    {
        _userContext = userContext;
        _dbContext = dbContext;
    }

    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}"/>
    public async Task<GetStoryResponse> Handle(GetStoryQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = _dbContext.Chats
            .Where(x => x.Id == request.Id && x.Users.Any(y => y.Id == _userContext.CurrentUserId));

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .SelectMany(x => x.Messages)
            .Select(x => new GetStoryResponseItem
            {
                Id = x.Id,
                Message = x.MessageText,
                Sender = x.User.UserName,
                SentDate = x.CreatedAt,
                SenderId = x.UserId,
                SenderImageId = x.User.UserPhotoId,
            })
            .OrderByDescending(x => x.SentDate)
            .SkipTake(request)
            .Reverse()
            .ToListAsync(cancellationToken);

        return new GetStoryResponse(result, totalCount);
    }
}