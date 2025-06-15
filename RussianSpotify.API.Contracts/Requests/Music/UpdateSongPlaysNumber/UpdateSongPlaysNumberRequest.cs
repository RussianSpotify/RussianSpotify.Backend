namespace RussianSpotify.Contracts.Requests.Music.UpdateSongPlaysNumber;

public class UpdateSongPlaysNumberRequest
{
    public UpdateSongPlaysNumberRequest()
    {
    }

    public UpdateSongPlaysNumberRequest(UpdateSongPlaysNumberRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        
        SongId = request.SongId;
    }
    
    public string SongId { get; set; }
}