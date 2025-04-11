namespace RussianSpotify.Contracts.Requests.Account.GetUserInfo;

public class UserPaymentHistoryItem
{
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}