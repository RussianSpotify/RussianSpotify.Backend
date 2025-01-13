#region

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RussianSpotify.API.Shared.Requests.File;

#endregion

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