using RussianSpotify.API.Files.Domain.Entities;
using RussianSpotify.API.Files.Models;
using RussianSpotify.API.Shared.Exceptions;

namespace RussianSpotify.API.Files.Exceptions;

/// <summary>
/// Ошибка на ненайденую сущность
/// </summary>
public class EntityNotFoundException<TEntity> : ApplicationBaseException
    where TEntity : class
{
    private static readonly IDictionary<Type, string> EntityExceptions = new Dictionary<Type, string>
    {
        [typeof(FileMetadata)] = "Не найдена сущность 'Файл'",
        [typeof(FileContent)] = "Не найден файл в S3 хранилище",
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