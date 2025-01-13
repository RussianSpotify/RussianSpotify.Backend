#region

using MediatR;
using RussianSpotify.Contracts.Requests.Chat.GetChats;

#endregion

namespace RussianSpotify.API.Core.Requests.Chat.GetChats;

/// <summary>
///     Запрос на получение чатов
/// </summary>
public class GetChatsQuery : IRequest<GetChatsResponse>
{
}