#region

using Microsoft.AspNetCore.SignalR;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.Contracts.Requests.Hub.CreateMessage;

#endregion

namespace RussianSpotify.API.Core.Services;

/// <summary>
///     Хаб чата
/// </summary>
public class ChatHub : Hub
{
    private readonly IChatService _chatService;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="chatService">Сервис чата</param>
    public ChatHub(IChatService chatService)
    {
        _chatService = chatService;
    }

    /// <summary>
    ///     Создание чата при подключении к хабу
    /// </summary>
    public override async Task OnConnectedAsync()
    {
        await _chatService.CreateChatAsync();
        await base.OnConnectedAsync();
    }

    /// <summary>
    ///     Отправить сообщение
    /// </summary>
    /// <param name="request">Запрос</param>
    public async Task SendMessage(CreateMessageRequest request)
    {
        var senderInfo = await _chatService.CreateMessageAsync(request);
        var receiverUsers = (await _chatService.GetUsersInChat(request.ChatId))
            .Select(x => x.ToString())
            .ToList();

        await Clients.Users(receiverUsers)
            .SendAsync("ReceiveMessage", new
            {
                request.Message,
                WhoSentUsername = senderInfo.Username,
                SenderId = senderInfo.Id,
            });
    }
}