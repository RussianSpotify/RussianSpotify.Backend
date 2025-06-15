namespace RussianSpotify.Contracts.Requests.Music.GetSongPlaysNumber;

public class GetSongPlaysNumberRequest
{
    public GetSongPlaysNumberRequest()
    {
    }

    public GetSongPlaysNumberRequest(GetSongPlaysNumberRequest request)
    {
        if(request == null)
            throw new ArgumentNullException(nameof(request));
        
        SongId = request.SongId;
    }
    
    public string SongId { get; set; }
}