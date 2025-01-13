#region

using System.Security.Claims;
using RussianSpotify.API.Core.Entities;

#endregion

namespace RussianSpotify.API.Core.Abstractions;

/// <summary>
///     Отвечает за клэймы юзера
/// </summary>
public interface IUserClaimsManager
{
    /// <summary>
    ///     Возвращает набор клэймов юзера
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>Лист клэймов юзера</returns>
    public List<Claim> GetUserClaims(User user);
}