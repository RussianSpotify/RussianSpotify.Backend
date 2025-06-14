using MediatR;
using RussianSpotify.Contracts.Requests.Music.UpdateSongPlaysNumber;

namespace RussianSpotify.API.Core.Requests.Music.UpdateSongPlaysNumber;

public class UpdateSongPlaysNumberCommand : UpdateSongPlaysNumberRequest, IRequest
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="request">Запрос</param>
    public UpdateSongPlaysNumberCommand(UpdateSongPlaysNumberRequest request) : base(request)
    {
    }
}