using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using RussianSpotify.API.Files.Requests.File;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.Shared.Requests.File;

namespace RussianSpotify.API.Shared.Services;

public class FileControllerHelper : IFileControllerHelper
{
    private const string DefaultContentDisposition = "attachment";
    
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileControllerHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Отправить файл на фронт
    /// </summary>
    /// <param name="file">Файл</param>
    /// <param name="headers">Заголовки</param>
    /// <param name="inline">В строку</param>
    /// <param name="customHeaders">Добавить кастомные заголовки</param>
    /// <returns>Файл</returns>
    public FileStreamResult GetFileStreamResult(
        BaseFileStreamResponse file,
        IHeaderDictionary headers,
        bool inline = false,
        bool customHeaders = false)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));

        var cd = new ContentDispositionHeaderValue(inline ? "inline" : DefaultContentDisposition);
        cd.SetHttpFileName(file.FileName);
        headers[HeaderNames.ContentDisposition] = cd.ToString();
        var response = _httpContextAccessor.HttpContext?.Response!;

        if (customHeaders)
        {
            response.Headers.ContentDisposition =
                new ContentDispositionHeaderValue(DefaultContentDisposition).ToString();
            response.ContentLength = file.Content.Length;
            response.Headers.AcceptRanges = "bytes";
            response.Headers.CacheControl = "max-age=14400";
        }

        if (file.Content.CanSeek)
            file.Content.Seek(0, SeekOrigin.Begin);

        return new FileStreamResult(file.Content, file.ContentType);
    }

    /// <summary>
    /// Отправить файл в байтах
    /// </summary>
    /// <param name="file">Файл</param>
    /// <returns>Файл в байтах</returns>
    public FileContentResult GetFileBytes(BaseFileBytesResponse file)
    {
        if (file is null)
            throw new ArgumentNullException(nameof(file));

        return new FileContentResult(fileContents: file.Content, contentType: file.ContentType)
        {
            FileDownloadName = file.FileName,
        };
    }

    /// <summary>
    /// Загрузить все файлы
    /// </summary>
    /// <param name="files">Файлы</param>
    /// <returns>Загруженные файлы</returns>
    public IEnumerable<UploadFileRequest> GetEnumerableFiles(List<IFormFile>? files)
    {
        foreach (var file in files ?? new List<IFormFile>())
        {
            using var stream = file.OpenReadStream();
            yield return new UploadFileRequest(stream, file.FileName, file.ContentType);
        }
    }
}