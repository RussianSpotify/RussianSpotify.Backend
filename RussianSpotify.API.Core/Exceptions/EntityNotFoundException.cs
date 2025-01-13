#region

using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Models;
using RussianSpotify.API.Shared.Exceptions;

#endregion

namespace RussianSpotify.API.Core.Exceptions;

/// <summary>
///     Ошибка на ненайденую сущность
/// </summary>
public class EntityNotFoundException<TEntity> : ApplicationBaseException
    where TEntity : class
{
    private static readonly IDictionary<Type, string> EntityExceptions = new Dictionary<Type, string>
    {
        [typeof(FileContent)] = "Не найден файл в S3 хранилище",
        [typeof(User)] = "Не найден пользователь",
        [typeof(Entities.Playlist)] = "Не найден альбом/плейлист",
        [typeof(Song)] = "Не найдена песня",
        [typeof(Chat)] = "Чат не найден"
    };

    public EntityNotFoundException(string message)
        : base($"{ExceptionEntity} с адресом {message}")
    {
    }

    public EntityNotFoundException(Guid id)
        : base($"{ExceptionEntity} c идентификатором {id}")
    {
    }

    private static string? ExceptionEntity => EntityExceptions.TryGetValue(typeof(TEntity), out var text)
        ? text
        : typeof(TEntity).FullName;
}