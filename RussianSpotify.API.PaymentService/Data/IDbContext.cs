using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RussianSpotify.API.PaymentService.Domain.Entities;

namespace RussianSpotify.API.PaymentService.Data;

public interface IDbContext
{
    /// <summary>
    /// Оплаты
    /// </summary>
    public DbSet<Payment> Payments { get; set; }
    
    /// <summary>
    ///     Сохранить изменения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Кол-во затронутых записей</returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    ///     Фасад базы
    /// </summary>
    public DatabaseFacade Database { get; }
}