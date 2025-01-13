#region

using MediatR;
using RussianSpotify.API.Files.Requests.File.GetImageById;

#endregion

namespace RussianSpotify.API.Files.Features.File.Queries.GetImageById;

/// <summary>
///     Запрос на получение фотографии
/// </summary>
public class GetImageByIdQuery : IRequest<GetImageByIdResponse>
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="id">ИД фотки</param>
    public GetImageByIdQuery(Guid id)
        => Id = id;

    /// <summary>
    ///     ИД фотки
    /// </summary>
    public Guid Id { get; set; }
}