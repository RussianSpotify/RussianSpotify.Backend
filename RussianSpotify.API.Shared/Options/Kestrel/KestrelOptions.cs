namespace RussianSpotify.API.Shared.Options.Kestrel;

// TODO: Закинуть эти Options в основной сервис тоже, чтобы была явная конфигурация Kestrel-а в Program.cs
public class KestrelOptions
{
    public KestrelOptionsItem[] Options { get; set; }
}