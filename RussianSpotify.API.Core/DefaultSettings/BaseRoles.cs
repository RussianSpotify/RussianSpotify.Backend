#region

using RussianSpotify.API.Core.Enums;
using RussianSpotify.API.Shared.Domain.Constants;

#endregion

namespace RussianSpotify.API.Core.DefaultSettings;

public static class BaseRoles
{
    /// <summary>
    ///     Базовые привилегии для ролей
    /// </summary>
    public static readonly IReadOnlyDictionary<Guid, List<Privileges>> RolePrivileges
        = new Dictionary<Guid, List<Privileges>>
        {
            [Roles.UserId] = new()
            {
                Privileges.ListenMusic
            },
            [Roles.AuthorId] = new()
            {
                Privileges.ListenMusic,
                Privileges.DeployMusic
            },
            [Roles.AdminId] = new()
            {
                Privileges.ListenMusic,
                Privileges.DeployMusic,
                Privileges.EditPages,
            }
        };
}