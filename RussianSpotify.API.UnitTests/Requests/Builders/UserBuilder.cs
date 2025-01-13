#region

using RussianSpotify.API.Core.Entities;

#endregion

namespace RussianSpotify.API.UnitTests.Requests.Builders;

/// <summary>
///     Builder пользователя
/// </summary>
internal class UserBuilder
{
    private readonly User _user = User.CreateForTest();

    private UserBuilder()
    {
    }

    /// <summary>
    ///     Создать builder
    /// </summary>
    /// <returns></returns>
    public static UserBuilder CreateBuilder()
        => new();

    /// <summary>
    ///     Установить имя
    /// </summary>
    /// <param name="username">Имя</param>
    public UserBuilder SetUsername(string username)
    {
        _user.UserName = username;
        return this;
    }

    /// <summary>
    ///     Добавить роли для пользователя
    /// </summary>
    /// <param name="roles">Роли</param>
    public UserBuilder SetRoles(List<Role> roles)
    {
        _user.Roles = roles;
        return this;
    }

    /// <summary>
    ///     Проставить ИД
    /// </summary>
    /// <param name="id">ИД</param>
    public UserBuilder SetId(Guid id)
    {
        _user.Id = id;
        return this;
    }

    /// <summary>
    ///     Установить email
    /// </summary>
    /// <param name="email">Имя</param>
    public UserBuilder SetEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    /// <summary>
    ///     Установить дату рождения
    /// </summary>
    /// <param name="birthday">Дата рождения</param>
    public UserBuilder SetBirthday(DateTime birthday)
    {
        _user.Birthday = birthday;
        return this;
    }

    public User Build() => _user;
}