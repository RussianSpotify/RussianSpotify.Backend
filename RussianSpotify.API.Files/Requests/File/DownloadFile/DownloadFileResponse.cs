#region

using RussianSpotify.API.Shared.Requests.File;

#endregion

namespace RussianSpotify.API.Files.Requests.File.DownloadFile;

/// <summary>
///     Ответ для <see cref="DownloadFileRequest" />
/// </summary>
public class DownloadFileResponse : BaseFileStreamResponse
{
    /// <inheritdoc />
    public DownloadFileResponse(
        Stream content,
        string fileName,
        string contentType)
        : base(content, fileName, contentType)
    {
    }
}