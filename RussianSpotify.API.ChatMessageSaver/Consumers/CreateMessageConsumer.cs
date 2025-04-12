#region

using MassTransit;
using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Models.ChatModels;

#endregion

namespace RussianSpotify.API.ChatMessageSaver.Consumers;

public class CreateMessageConsumer : IConsumer<CreateMessageModel>
{
    private readonly IDbContext _dbContext;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    public CreateMessageConsumer(IDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Consume(ConsumeContext<CreateMessageModel> context)
    {
        var request = context.Message;

        var currentUser = await _dbContext.Users
                              .Include(x => x.Chats)
                              .FirstOrDefaultAsync(x => x.Id == request.UserId)
                          ?? throw new ForbiddenException();

        var chat = currentUser.Chats
                       .FirstOrDefault(x => x.Id == request.ChatId)
                   ?? throw new EntityNotFoundException<Chat>(request.ChatId);

        var message = new Message(
            context.Message.Message,
            currentUser,
            chat);

        await _dbContext.Messages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }
}