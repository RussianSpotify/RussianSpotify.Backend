#region

using MediatR;
using Microsoft.EntityFrameworkCore;
using Payments;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Shared.Domain.Constants;
using RussianSpotify.API.Shared.Exceptions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.Contracts.Requests.Account.GetUserInfo;

#endregion

namespace RussianSpotify.API.Core.Requests.Account.GetUserInfo;

/// <summary>
///     Обработчик для <see cref="GetUserInfoQuery" />
/// </summary>
public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoResponse>
{
    private readonly IUserContext _userContext;
    private readonly IDbContext _dbContext;
    
    private readonly PaymentService.PaymentServiceClient _paymentService;

    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="userContext">Контекс текущего пользоавтеля</param>
    /// <param name="dbContext">Интерфейс контекста бд</param>
    /// <param name="paymentService">Grpc клиент сервиса оплата</param>
    public GetUserInfoQueryHandler(
        IUserContext userContext,
        IDbContext dbContext, 
        PaymentService.PaymentServiceClient paymentService)
    {
        _userContext = userContext;
        _dbContext = dbContext;
        _paymentService = paymentService;
    }

    /// <inheritdoc cref="IRequestHandler{TRequest,TResponse}" />
    public async Task<GetUserInfoResponse> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await _dbContext.Users
                .Include(x => x.Roles)
                .Include(x => x.Chats)
                .FirstOrDefaultAsync(x => x.Id == _userContext.CurrentUserId, cancellationToken)
                ?? throw new ForbiddenException();
        
        var payments = await _paymentService.GetPaymentHistoryAsync(new GetPaymentHistoryRequest
        {
            UserId = user.Id.ToString()
        });

        return new GetUserInfoResponse
        {
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            UserPhotoId = user.UserPhotoId,
            ChatId = user.Roles.Any(x => x.Name == Roles.AdminRoleName)
                ? null
                : user.Chats.FirstOrDefault()
                    ?.Id,
            PaymentHistory = new UserPaymentHistory
            {
                Items = payments.Items.Select(x => new UserPaymentHistoryItem
                    {
                        Amount = (decimal)x.Amount,
                        CreatedAt = x.CreatedAt.ToDateTime()
                    })
                .ToList()
            },
            Roles = user.Roles
                .Select(x => x.Name)
                .ToList(),
        };
    }
}