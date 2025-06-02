using MediatR;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Extensions;
using RussianSpotify.API.Core.Requests.Chat.GetStory;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Chat.GetStory;

namespace RussianSpotify.API.Core.Requests.Chat.GetHistoryGrpc;

public class GetHistoryGrpcQueryHandler : IRequestHandler<GetHistoryGrpcQuery, GetStoryResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;

    public GetHistoryGrpcQueryHandler(IDbContext dbContext, IUserContext userContext)
    {
        _dbContext = dbContext;
        _userContext = userContext;
    }

    public async Task<GetStoryResponse> Handle(GetHistoryGrpcQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var query = _dbContext.Messages
            .Where(x => x.ReceiverId == _userContext.CurrentUserId);

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .Select(x => new GetStoryResponseItem
            {
                Id = x.Id,
                Message = x.MessageText,
                Sender = x.User.UserName,
                SentDate = x.CreatedAt,
                SenderId = x.UserId,
            })
            .OrderByDescending(x => x.SentDate)
            .SkipTake(request)
            .Reverse()
            .ToListAsync(cancellationToken);

        return new GetStoryResponse(result, totalCount);
    }
}