using MediatR;
using RussianSpotify.Contracts.Requests.Music.GetSongPlaysNumber;

namespace RussianSpotify.API.Core.Requests.Music.GetSongPlaysNumber;

public class GetSongPlaysNumberQuery : GetSongPlaysNumberRequest, IRequest<int>
{
    public GetSongPlaysNumberQuery(GetSongPlaysNumberRequest request) : base(request)
    {
    }
}