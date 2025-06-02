using System.Collections.Concurrent;
using Grpc.Core;
using RussianSpotify.API.Grpc;
using Google.Protobuf.WellKnownTypes;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Hub.CreateMessage;

namespace RussianSpotify.API.Core.Services.Grpc;

public class GrpcChatService : API.Grpc.ChatService.ChatServiceBase
{
    private readonly IChatService _chatService;
    private readonly IUserContext _userContext;

    public GrpcChatService(IChatService chatService, IUserContext userContext)
    {
        _chatService = chatService;
        _userContext = userContext;
    }

    // Храним всех подключённых пользователей: userId -> stream
    private static readonly ConcurrentDictionary<string, IServerStreamWriter<SendMessageStreamResponse>> ConnectedUsers = new();

    public override async Task SendMessageStream(
        IAsyncStreamReader<SendMessageStreamRequest> requestStream,
        IServerStreamWriter<SendMessageStreamResponse> responseStream,
        ServerCallContext context)
    {
        // Получаем sender_id из метаданных (или авторизации)
        var senderId = _userContext.CurrentUserId!.Value;

        // Добавляем пользователя в словарь
        ConnectedUsers[senderId.ToString()] = responseStream;

        try
        {
            await foreach (var request in requestStream.ReadAllAsync(context.CancellationToken))
            {
                Console.WriteLine($"Message from {senderId} to {request.ReceiverId}: {request.Content}");

                await _chatService.CreateMessageAsync(new CreateMessageRequest
                {
                    Message = request.Content,
                    ChatId = null,
                    ReceiverId = new Guid(request.ReceiverId)
                });
                
                // Формируем сообщение
                var response = new SendMessageStreamResponse
                {
                    SenderId = senderId.ToString(),
                    Username = "mock_username", // Подставить имя из профиля
                    Content = request.Content
                };

                // Ищем, подключён ли receiver
                if (ConnectedUsers.TryGetValue(request.ReceiverId, out var receiverStream))
                {
                    await receiverStream.WriteAsync(response);
                }
                else
                {
                    Console.WriteLine($"Receiver {request.ReceiverId} not connected.");
                    // Можно логировать/сохранять сообщение в БД
                }
            }
        }
        catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
        {
            Console.WriteLine($"{senderId} disconnected.");
        }
        finally
        {
            ConnectedUsers.TryRemove(senderId.ToString(), out _);
        }
    }
}
