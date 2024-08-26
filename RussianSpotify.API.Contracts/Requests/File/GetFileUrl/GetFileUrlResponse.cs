namespace RussianSpotify.Contracts.Requests.File.GetFileUrl;

/// <summary>
/// Ответ на запрос о получении файла в виде URL
/// </summary>
public class GetFileUrlResponse
{
    /// <summary>
    /// Файл в виде URL
    /// </summary>
    public string FileUrl { get; set; }
}