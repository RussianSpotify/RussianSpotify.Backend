namespace RussianSpotify.Contracts.Requests.Auth.PostRegister;

public class PostRegisterResponse
{
    /// <summary>
    ///     Конструктор
    /// </summary>
    /// <param name="email">Почта</param>
    public PostRegisterResponse(string email)
        => Email = email;

    /// <summary>
    ///     Почта пользователя
    /// </summary>
    public string Email { get; set; }
}