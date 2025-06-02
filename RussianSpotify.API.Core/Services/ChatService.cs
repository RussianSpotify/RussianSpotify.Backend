#region

using MassTransit;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Domain.Constants;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.Shared.Models.ChatModels;
using RussianSpotify.Contracts.Requests.Chat.GetSenderMessage;
using RussianSpotify.Contracts.Requests.Hub.CreateMessage;

#endregion

namespace RussianSpotify.API.Core.Services;

/// <summary>
///     Сервис чата
/// </summary>
public class ChatService : IChatService
{
    private readonly IDbContext _dbContext;
    private readonly IUserContext _userContext;
    private readonly IBus _bus;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    /// <param name="userContext">Контекст пользователя</param>
    /// <param name="bus">Очередь в памяти</param>
    public ChatService(
        IDbContext dbContext,
        IUserContext userContext,
        IBus bus)
    {
        _dbContext = dbContext;
        _userContext = userContext;
        _bus = bus;
    }

    /// <inheritdoc />
    public async Task CreateChatAsync()
    {
        var currentUser = await _dbContext.Users
                              .Include(x => x.Roles)
                              .Include(x => x.Chats)
                              .FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId)
                          ?? throw new ForbiddenException();

        var admins = await _dbContext.Users
            .Where(x => x.Roles.Any(y => y.Name == Roles.AdminRoleName))
            .ToListAsync();

        var chatExists = currentUser.Chats.Any();

        var isAdmin = currentUser.Roles
            .Any(x => x.Name == Roles.AdminRoleName);

        if (chatExists || isAdmin)
            return;

        var chat = new Chat(
            name: currentUser.UserName,
            users: new List<User>(admins)
            {
                currentUser
            });

        await _dbContext.Chats.AddAsync(chat);
        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<GetSenderMessageInfo> CreateMessageAsync(CreateMessageRequest model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var currentUser = await _dbContext.Users
                              .AsNoTracking()
                              .FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId)
                          ?? throw new ForbiddenException();

        await _bus.Publish(new CreateMessageModel
        {
            Message = model.Message,
            ChatId = model.ChatId,
            UserId = currentUser.Id,
            ReceiverId = model.ReceiverId
        });

        return new GetSenderMessageInfo
        {
            Id = currentUser.Id,
            Username = currentUser.UserName,
            ImageId = currentUser.UserPhotoId
        };
    }

    /// <inheritdoc />
    public async Task<List<Guid>> GetUsersInChat(Guid chatId)
        => await _dbContext.Chats
            .Where(x => x.Id == chatId && x.Users.Any(y => y.Id == _userContext.CurrentUserId))
            .SelectMany(y => y.Users)
            .Select(x => x.Id)
            .ToListAsync();
}