using MediatR;
using RussianSpotify.Contracts.Models;
using RussianSpotify.Contracts.Requests.Chat.GetStory;

namespace RussianSpotify.API.Core.Requests.Chat.GetHistoryGrpc;

public class GetHistoryGrpcQuery : GetStoryRequest, IRequest<GetStoryResponse>, IPaginationFilter
{
    
}