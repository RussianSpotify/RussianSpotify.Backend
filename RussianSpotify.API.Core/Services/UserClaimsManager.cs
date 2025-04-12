#region

using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Services;

/// <inheritdoc cref="IUserClaimsManager" />
public class UserClaimsManager : IUserClaimsManager
{
    /// <inheritdoc cref="IUserClaimsManager" />
    public List<Claim> GetUserClaims(User user)
    {
        if (user.Roles is null)
            throw new NotIncludedException(nameof(user.Roles));

        var userRoles = user.Roles.ToList();

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        return authClaims;
    }
}