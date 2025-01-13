#region

using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Services;

/// <inheritdoc cref="IRoleManager" />
public class RoleManager : IRoleManager
{
    /// <inheritdoc cref="IRoleManager" />
    public bool IsInRole(User user, string roleName)
    {
        if (user.Roles is null)
            throw new NotIncludedException(nameof(user.Roles));

        var roles = user.Roles
            .Select(x => x.Name)
            .ToList();

        return roles.Contains(roleName);
    }
}