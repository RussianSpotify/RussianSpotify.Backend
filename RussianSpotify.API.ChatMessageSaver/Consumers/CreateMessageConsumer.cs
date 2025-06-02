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
        

        var message = new Message(
            context.Message.Message,
            currentUser);

        if (context.Message.ChatId is not null)
        {
            var chat = currentUser.Chats
                           .FirstOrDefault(x => x.Id == request.ChatId)
                       ?? throw new EntityNotFoundException<Chat>(request.ChatId!.Value);
            
            message.Chat = chat;
        }

        if (context.Message.ReceiverId is not null)
        {
            var receiverUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.UserId) 
                               ?? throw new ForbiddenException();
            
            message.Receiver = receiverUser;
        }

        await _dbContext.Messages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }
}