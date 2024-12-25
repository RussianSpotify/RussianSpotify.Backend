using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RussianSpotify.API.Files.Requests.File;
using RussianSpotify.API.Shared.Requests.File;

namespace RussianSpotify.API.Shared.Interfaces;

public interface IFileControllerHelper
{
    FileStreamResult GetFileStreamResult(
        BaseFileStreamResponse file,
        IHeaderDictionary headers,
        bool inline = false,
        bool customHeaders = false);

    FileContentResult GetFileBytes(BaseFileBytesResponse file);

    IEnumerable<UploadFileRequest> GetEnumerableFiles(List<IFormFile>? files);
}