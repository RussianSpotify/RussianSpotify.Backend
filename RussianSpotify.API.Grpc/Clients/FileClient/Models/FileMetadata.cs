namespace RussianSpotify.API.Grpc.Clients.FileClient.Models;

public class FileMetadata
{
    public Guid Id { get; set; } = Guid.Empty;

    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;

    public Guid UserId { get; set; } = Guid.Empty;

    public string Address { get; set; } = string.Empty;
}