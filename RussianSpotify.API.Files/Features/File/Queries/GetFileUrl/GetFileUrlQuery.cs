using MediatR;
using RussianSpotify.API.Files.Requests.File.GetFileUrl;

namespace RussianSpotify.API.Files.Features.File.Queries.GetFileUrl;

/// <summary>
/// Запрос на получение файла в виде URL
/// </summary>
public class GetFileUrlQuery : IRequest<GetFileUrlResponse>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор файла</param>
    public GetFileUrlQuery(Guid id)
        => Id = id;

    /// <summary>
    /// Идентификатор файла
    /// </summary>
    public Guid Id { get; set; }
}