using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Payments;
using RussianSpotify.API.PaymentService.Data;

namespace RussianSpotify.API.PaymentService.GrpcServices;

public class PaymentService : Payments.PaymentService.PaymentServiceBase
{
    private readonly IDbContext _dbContext;

    public PaymentService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<GetPaymentHistoryResponse> GetPaymentHistory(GetPaymentHistoryRequest request, ServerCallContext context)
    {
        try
        {
            if (!Guid.TryParse(request.UserId, out var userId))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid user ID format."));

            var payments = await _dbContext.Payments
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(context.CancellationToken);

            var response = new GetPaymentHistoryResponse();
            response.Items.AddRange(payments.Select(x => new GetPaymentHistoryResponseItem
            {
                CreatedAt = x.CreatedAt.ToTimestamp(),
                Amount = (double)x.Amount
            }));

            return response;
        }
        catch (RpcException) { throw; }
        catch (Exception ex)
        {
            throw new RpcException(new Status(StatusCode.Internal, $"Unexpected error occurred: {ex.Message}"));
        }
    }
}