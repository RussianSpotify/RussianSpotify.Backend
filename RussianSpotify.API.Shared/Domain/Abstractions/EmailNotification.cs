namespace RussianSpotify.API.Shared.Domain.Abstractions;

public abstract class EmailNotification
{
    public string Body { get; set; }

    public string Head { get; set; }

    public string Type { get; set; }
    
    public abstract EmailNotification CreateNotification();
}