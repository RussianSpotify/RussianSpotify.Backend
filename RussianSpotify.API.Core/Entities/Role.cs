using RussianSpotify.API.Core.Enums;
using RussianSpotify.API.Core.Exceptions;
using RussianSpotify.API.Shared.Domain.Abstractions;

namespace RussianSpotify.API.Core.Entities;

public class Role : BaseEntity
{
    private string _name = default!;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public Role()
    {
        Privileges = new List<RolePrivilege>();
        Users = new List<User>();
    }

    /// <summary>
    /// Название роли
    /// </summary>
    public string Name
    { 
        get => _name;
        set => _name = string.IsNullOrWhiteSpace(value)
            ? throw new RequiredFieldException("Логин пользователя")
            : value;
    }

    /// <summary>
    /// Привилегии
    /// </summary>
    public List<RolePrivilege> Privileges { get; protected set; }

    /// <summary>
    /// Пользователи
    /// </summary>
    public List<User>? Users { get; set; }

    /// <summary>
    /// Создать тестовую сущность
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="name">Название роли</param>
    /// <param name="rolePrivileges">Привилегии данной роли</param>
    /// <param name="users">Пользователи</param>
    /// <returns>Роль</returns>
    [Obsolete("Только для тестов")]
    public static Role CreateForTest(
        Guid id = default,
        string? name = default,
        List<RolePrivilege>? rolePrivileges = default,
        List<User>? users = default)
        => new()
        {
            Id = id,
            _name = name ?? string.Empty,
            Privileges = rolePrivileges ?? new(),
            Users = users ?? new(),
        };

    /// <summary>
    /// Обновить привилегии
    /// </summary>
    /// <param name="privilegesList">Список привилегий</param>
    public void UpdatePrivileges(List<Privileges> privilegesList)
    {
        if (Privileges is null)
            throw new NotIncludedException(nameof(Privileges));

        var rolePrivilegesToDelete = Privileges.Where(x =>
                !privilegesList.Exists(y => y == x.Privilege))
            .ToList();

        foreach (var rolePrivilegeToDelete in rolePrivilegesToDelete)
            Privileges.Remove(rolePrivilegeToDelete);

        foreach (var privilegeToUpdate in privilegesList)
            if (!Privileges.Exists(x => x.Privilege == privilegeToUpdate))
                Privileges.Add(new RolePrivilege(this, privilegeToUpdate));
    }
}