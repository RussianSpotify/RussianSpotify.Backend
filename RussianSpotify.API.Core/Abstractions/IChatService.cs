using RussianSpotify.API.Shared.Models.ChatModels;
using RussianSpotify.Contracts.Requests.Chat.GetSenderMessage;
using RussianSpotify.Contracts.Requests.Hub.CreateMessage;

namespace RussianSpotify.API.Core.Abstractions;

/// <summary>
/// Сервис чата
/// </summary>
public interface IChatService
{
    /// <summary>
    /// Создать чат
    /// </summary>
    /// <returns>Идентификатор чата</returns>
    public Task CreateChatAsync();

    /// <summary>
    /// Создать сообщение
    /// </summary>
    /// <returns>Данные отправителя</returns>
    public Task<GetSenderMessageInfo> CreateMessageAsync(CreateMessageRequest model);

    /// <summary>
    /// Получить пользователей чата
    /// </summary>
    /// <param name="chatId">Идентификатор чата</param>
    /// <returns>Список пользователей</returns>
    public Task<List<Guid>> GetUsersInChat(Guid chatId);
}