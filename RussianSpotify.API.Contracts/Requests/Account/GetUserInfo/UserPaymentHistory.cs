namespace RussianSpotify.Contracts.Requests.Account.GetUserInfo;

public class UserPaymentHistory
{
    public IEnumerable<UserPaymentHistoryItem> Items { get; set; }
}