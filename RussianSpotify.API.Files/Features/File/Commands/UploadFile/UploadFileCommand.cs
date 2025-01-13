#region

using MediatR;
using RussianSpotify.API.Files.Requests.File.UploadFile;

#endregion

namespace RussianSpotify.API.Files.Features.File.Commands.UploadFile;

/// <summary>
///     Команда на загрузку файла
/// </summary>
public class UploadFileCommand : UploadRequest, IRequest<UploadFileResponse>
{
    /// <inheritdoc />
    public UploadFileCommand(IEnumerable<UploadRequestItem> files)
        : base(files)
    {
    }
}