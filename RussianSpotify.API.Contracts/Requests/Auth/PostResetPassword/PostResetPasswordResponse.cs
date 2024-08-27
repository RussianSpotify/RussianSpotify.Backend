namespace RussianSpotify.Contracts.Requests.Auth.PostResetPassword;

/// <summary>
/// Ответ на запрос сброса пароля
/// </summary>
public class PostResetPasswordResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="email">Почта</param>
    public PostResetPasswordResponse(string email)
        => Email = email;

    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; }
}