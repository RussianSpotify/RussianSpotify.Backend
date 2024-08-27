namespace RussianSpotify.API.Core.Exceptions;

/// <summary>
/// Обязательные поля
/// </summary>
public class RequiredFieldException : ApplicationException
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="field">Поле</param>
    public RequiredFieldException(string field)
        : base($"Поле {field} является обязательным для заполнения.")
    {
    }
}