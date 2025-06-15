namespace RussianSpotify.API.Shared.Models.SongEvents;

public class PlaysNumberUpdatedEvent
{
    public string SongId { get; set; }
    
    public uint CurrentPlaysNumber { get; set; }
}