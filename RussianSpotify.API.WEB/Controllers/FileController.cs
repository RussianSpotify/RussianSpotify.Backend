using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using RussianSpotify.API.Core.Requests.File.DeleteFile;
using RussianSpotify.API.Core.Requests.File.DownloadFile;
using RussianSpotify.API.Core.Requests.File.GetFileUrl;
using RussianSpotify.API.Core.Requests.File.GetImageById;
using RussianSpotify.API.Core.Requests.File.UploadFile;
using RussianSpotify.Contracts.Requests.File.DeleteFile;
using RussianSpotify.Contracts.Requests.File.GetFileUrl;
using RussianSpotify.Contracts.Requests.File.UploadFile;

namespace RussianSpotify.API.WEB.Controllers;

/// <summary>
/// Контроллер для файлов
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class FileController : FileBaseController
{
    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <param name="files">Файлы</param>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<UploadFileResponse> UploadFileAsync(
        [FromForm] List<IFormFile>? files,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
        => await mediator.Send(new UploadFileCommand(GetEnumerableFiles(files)), cancellationToken);

    /// <summary>
    /// Скачать файл
    /// </summary>
    /// <param name="id">ИД файла</param>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpGet("{id}/Download")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<FileStreamResult> DownloadFileAsync(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var file = await mediator.Send(new DownloadFileQuery(id), cancellationToken);

        return GetFileStreamResult(file, Response.Headers);
    }

    /// <summary>
    /// Получить файл в виде URL
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="id">Идентификатор файла</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Файл в виде URL</returns>
    [HttpGet("{id}/GetFileUrl")]
    [ProducesResponseType(type: typeof(GetFileUrlResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<GetFileUrlResponse> GetFileUrlAsync(
        [FromServices] IMediator mediator,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetFileUrlQuery(id), cancellationToken);

    /// <summary>
    /// Получить изображение по ИД
    /// </summary>
    /// <param name="id">Ид изображения</param>
    /// <param name="mediator"></param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Бинарные данные</returns>
    [HttpGet("Image/{id}")]
    [AllowAnonymous]
    public async Task<FileContentResult> GetImageByIdAsync(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetImageByIdQuery(id), cancellationToken);

        return GetFileBytes(file: result);
    }

    /// <summary>
    /// Эндпоинт для удаления файла
    /// </summary>
    /// <param name="request">Запрос с информацией</param>
    /// <param name="mediator">Медиатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    [HttpDelete("Delete")]
    public async Task DeleteFileAsync(
        [FromQuery] DeleteFileRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var query = new DeleteFileCommand(request);
        await mediator.Send(query, cancellationToken);
    }
}