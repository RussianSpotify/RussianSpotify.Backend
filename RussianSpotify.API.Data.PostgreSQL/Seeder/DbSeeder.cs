#region

using Microsoft.EntityFrameworkCore;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Shared.Domain.Constants;
using RussianSpotify.Contracts.Enums;

#endregion

namespace RussianSpotift.API.Data.PostgreSQL.Seeder;

/// <summary>
///     Сид базовых значений в бд
/// </summary>
public class DbSeeder : IDbSeeder
{
    private static readonly IReadOnlyDictionary<Guid, string> BaseRoles = new Dictionary<Guid, string>
    {
        [Roles.AdminId] =
            Roles.AdminRoleName,

        [Roles.AuthorId] =
            Roles.AuthorRoleName,

        [Roles.UserId] =
            Roles.UserRoleName
    };

    private static readonly List<CategoryType> _baseCategories = new()
    {
        CategoryType.HipHop,
        CategoryType.Metall,
        CategoryType.Rap,
        CategoryType.Rock
    };

    /// <inheritdoc />
    public async Task SeedAsync(IDbContext efContext, CancellationToken cancellationToken)
    {
        await SeedRoleAsync(efContext, cancellationToken);
        await SeedPrivilegesAsync(efContext, cancellationToken);
        await SeedCategoriesAsync(efContext, cancellationToken);
        await efContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SeedRoleAsync(IDbContext dbContext, CancellationToken cancellationToken)
    {
        var existsRolesInDb = await dbContext.Roles
            .Where(x => BaseRoles.Keys.Contains(x.Id))
            .Select(x => x.Id)
            .ToListAsync(cancellationToken) ?? new List<Guid>();

        var rolesToSeed = BaseRoles
            .Where(x => !existsRolesInDb.Contains(x.Key))
            .Select(x => new Role { Id = x.Key, Name = x.Value })
            .ToList();

        rolesToSeed.ForEach(x =>
        {
            if (!RussianSpotify.API.Core.DefaultSettings.BaseRoles.RolePrivileges.TryGetValue(x.Id, out var privileges))
                throw new ArgumentException("Не найдена для данной роли привилегий");

            x.UpdatePrivileges(privileges);
        });

        await dbContext.Roles.AddRangeAsync(rolesToSeed, cancellationToken);
    }

    private static async Task SeedPrivilegesAsync(IDbContext dbContext, CancellationToken cancellationToken)
    {
        var existsRolesInDb = await dbContext.Roles
            .Include(x => x.Privileges)
            .Where(x => RussianSpotify.API.Core.DefaultSettings.BaseRoles.RolePrivileges.Keys.Contains(x.Id))
            .ToListAsync(cancellationToken);

        existsRolesInDb.ForEach(x =>
        {
            if (!RussianSpotify.API.Core.DefaultSettings.BaseRoles.RolePrivileges.TryGetValue(x.Id, out var privileges))
                throw new ArgumentException("Не найдена для данной роли привилегий");

            var currentPriviles = x.Privileges.Select(y => y.Privilege).ToList();
            currentPriviles.AddRange(privileges);
            currentPriviles = currentPriviles.Distinct().ToList();

            x.UpdatePrivileges(currentPriviles);
        });
    }

    private static async Task SeedCategoriesAsync(IDbContext dbContext, CancellationToken cancellationToken)
    {
        var existsCategoriesInDb = await dbContext.Categories
            .ToListAsync(cancellationToken);

        var categoriesToDelete = existsCategoriesInDb
            .Where(categoryFromDb => _baseCategories.All(x => x != categoryFromDb.CategoryName))
            .ToList();

        foreach (var categoryToDelete in categoriesToDelete)
            dbContext.Categories.Remove(categoryToDelete);

        _baseCategories.ForEach(x =>
        {
            if (existsCategoriesInDb.All(y => y.CategoryName != x))
            {
                dbContext.Categories.Add(new Category
                {
                    CategoryName = x
                });
            }
        });
    }
}