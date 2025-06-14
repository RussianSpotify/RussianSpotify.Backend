namespace RussianSpotify.API.Shared.Models.SongEvents;

public class PlaysNumberUpdatedEvent
{
    public Guid SongId { get; set; }
    
    public uint CurrentPlaysNumber { get; set; }
}