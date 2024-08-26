using MediatR;
using RussianSpotify.Contracts.Requests.File.GetFileUrl;

namespace RussianSpotify.API.Core.Requests.File.GetFileUrl;

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