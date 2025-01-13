namespace RussianSpotify.API.Grpc.Clients.FileClient.Models;

public class File
{
    public Stream Content { get; set; }

    public FileMetadata Metadata { get; set; }

    [Obsolete("Только для тестов")]
    public static File CreateForTest(
        Stream? content = default,
        string fileName = "testFile",
        string contentType = ".mp3")
        => new()
        {
            Content = content ?? new MemoryStream(),
            Metadata = new FileMetadata
            {
                FileName = fileName,
                ContentType = contentType
            }
        };
}