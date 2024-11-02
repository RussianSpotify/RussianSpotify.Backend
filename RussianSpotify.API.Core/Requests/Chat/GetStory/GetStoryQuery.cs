using MediatR;
using RussianSpotify.Contracts.Models;
using RussianSpotify.Contracts.Requests.Chat.GetStory;

namespace RussianSpotify.API.Core.Requests.Chat.GetStory;

/// <summary>
/// Получить историю чата
/// </summary>
public class GetStoryQuery : GetStoryRequest, IRequest<GetStoryResponse>, IPaginationFilter
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id"></param>
    public GetStoryQuery(Guid id) =>
        Id = id;

    /// <summary>
    /// Идентификатор чата
    /// </summary>
    public Guid Id { get; set; }
}