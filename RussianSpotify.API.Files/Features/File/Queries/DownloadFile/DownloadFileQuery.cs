using MediatR;
using RussianSpotify.API.Files.Requests.File.DownloadFile;
using RussianSpotify.Contracts.Requests.File.DownloadFile;

namespace RussianSpotify.API.Files.Features.File.Queries.DownloadFile;

/// <summary>
/// Запрос на получение файла
/// </summary>
public class DownloadFileQuery : DownloadFileRequest, IRequest<DownloadFileResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">ИД файла</param>
    public DownloadFileQuery(Guid id)
        : base(id)
    {
    }
}