using MediatR;

namespace RussianSpotify.API.Core.Requests.Music.GetSongPlaysNumber;

public class GetSongPlaysNumberQuery : IRequest<int>
{
    public GetSongPlaysNumberQuery(string id)
    {
        SongId = id;
    }

    public string SongId { get; set; }
}