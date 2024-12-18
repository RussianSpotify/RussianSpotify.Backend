using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Moq;
using RussianSpotift.API.Data.PostgreSQL;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Grpc.Clients.FileClient;
using RussianSpotify.API.Grpc.Clients.FileClient.Models;
using RussianSpotify.API.Shared.Domain.Constants;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.UnitTests.Requests.Builders;

namespace RussianSpotify.API.UnitTests;

/// <summary>
/// Базовая конфигурация для тестов
/// </summary>
public class UnitTestBase : IDisposable
{
    private const string CurrentUserId = "53afbb05-bb2d-45e0-8bef-489ef1cd6fdc";

    /// <summary>
    /// Тестовый код для редиса (в тестах использовать его)
    /// </summary>
    protected const string CodeForRedis = "111111111111";
    
    /// <summary>
    /// Пользователь для теста
    /// </summary>
    protected User User { get; private set; }
    
    /// <summary>
    /// Мок сервиса дат
    /// </summary>
    protected Mock<IDateTimeProvider> DateTimeProvider { get; private set; }
    
    /// <summary>
    /// Мок сервиса токенов
    /// </summary>
    protected Mock<IJwtGenerator> JwtGenerator { get; }
    
    /// <summary>
    /// Мок контекст пользователя
    /// </summary>
    protected Mock<IUserContext> UserContext { get; }
    
    /// <summary>
    /// Мок сервиса по работе с подпиской
    /// </summary>
    protected Mock<ISubscriptionHandler> SubscriptionService { get; }
    
    /// <summary>
    /// Мок Взаимодействия с ролью пользователя
    /// </summary>
    protected Mock<IRoleManager> RoleManager { get; }
    
    /// <summary>
    /// Мок S3 Service
    /// </summary>
    protected Mock<IFileServiceClient> S3Service { get; }
    
    /// <summary>
    /// Мок Сервис для работы с паролями
    /// </summary>
    protected Mock<IPasswordService> PasswordService { get; }
    
    /// <summary>
    /// Мок Фабрика токенов для почты
    /// </summary>
    protected Mock<ITokenFactory> TokenFactory { get; }
    
    /// <summary>
    /// Мок сервиса почты
    /// </summary>
    protected Mock<IEmailSender> EmailSender { get; }
    
    /// <summary>
    /// Мок Кеша
    /// </summary>
    protected Mock<IDistributedCache> Cache { get; }
    
    /// <summary>
    /// Мок Отвечает за клэймы юзера
    /// </summary>
    protected Mock<IUserClaimsManager> UserClaimsManager { get; } 
    
    /// <summary>
    /// Конструктор
    /// </summary>
    protected UnitTestBase()
    {
        User = UserBuilder
            .CreateBuilder()
            .SetRoles(new List<Role>()
            {
                new()
                {
                    Name = Roles.AdminRoleName
                }
            })
            .SetId(Guid.Parse(CurrentUserId))
            .SetUsername("Test Login")
            .SetBirthday(new DateTime(2004, 02, 12))
            .SetEmail("test@mail.ru")
            .Build();

        UserClaimsManager = new Mock<IUserClaimsManager>();
        UserClaimsManager.Setup(x => x.GetUserClaims(It.IsAny<User>()))
            .Returns(new List<Claim>()
            {
                new(ClaimTypes.Role, "хуй")
            });

        EmailSender = new Mock<IEmailSender>();
        EmailSender.Setup(
                x => x.SendEmailAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        Cache = new Mock<IDistributedCache>();
        Cache.Setup(
                x => x.SetAsync(
                    It.IsAny<string>(),
                    It.IsAny<byte[]>(),
                    It.IsAny<DistributedCacheEntryOptions>(),
                    It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        Cache.Setup(
                x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Encoding.UTF8.GetBytes(CodeForRedis));
        Cache.Setup(x => x.RemoveAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        PasswordService = new Mock<IPasswordService>();
        PasswordService
            .Setup(x => x.HashPassword(It.IsAny<string>()))
            .Returns("1213");
        PasswordService
            .Setup(x => x.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        TokenFactory = new Mock<ITokenFactory>();
        TokenFactory.Setup(x => x.GetToken())
            .Returns("22222222");

        S3Service = new Mock<IFileServiceClient>();
        S3Service.Setup(x => x.GetFileAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Grpc.Clients.FileClient.Models.File.CreateForTest());
        S3Service.Setup(x => x.GetFileMetadataAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Grpc.Clients.FileClient.Models.File.CreateForTest().Metadata);
        
        S3Service.Setup(x => x.IsImage(It.IsAny<string>()))
            .Returns(true);
        S3Service.Setup(
                x => x.DeleteAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
            .Returns(() => Task.CompletedTask);
        
        DateTimeProvider = new Mock<IDateTimeProvider>();
        DateTimeProvider.Setup(x => x.CurrentDate)
            .Returns(new DateTime(2004, 12, 12));

        JwtGenerator = new Mock<IJwtGenerator>();
        JwtGenerator.Setup(x => x.GenerateToken(It.IsAny<List<Claim>>()))
            .Returns(Guid.NewGuid().ToString);
        JwtGenerator.Setup(x => x.GenerateRefreshToken())
            .Returns(Guid.NewGuid().ToString);
        JwtGenerator.Setup(x => x.GetPrincipalFromExpiredToken(It.IsAny<string>()))
            .Returns(new ClaimsPrincipal(identities: new List<ClaimsIdentity>()
            {
                new(claims: new List<Claim>()
                {
                    new(ClaimTypes.Email, User.Email)
                })
            }));

        UserContext = new Mock<IUserContext>();
        UserContext.Setup(x => x.CurrentUserId)
            .Returns(Guid.Parse(CurrentUserId));

        SubscriptionService = new Mock<ISubscriptionHandler>();
        SubscriptionService.Setup(x => x.GetSubscription(It.IsAny<Guid>()))
            .Verifiable();
        SubscriptionService.Setup(x => x.Subscribe(It.IsAny<Guid>(), It.IsAny<int>()))
            .Verifiable();

        RoleManager = new Mock<IRoleManager>();
        RoleManager.Setup(x => x.IsInRole(
                It.IsAny<User>(),
                It.IsAny<string>()))
            .Returns(true);
    }

    /// <summary>
    /// Сконфигурировать сервис дат, на выдачу конкретной даты
    /// </summary>
    /// <param name="dateTime">Дата</param>
    protected void SetUpCustomDate(DateTime dateTime)
    {
        DateTimeProvider = new Mock<IDateTimeProvider>();
        DateTimeProvider.Setup(x => x.CurrentDate)
            .Returns(dateTime);
    }

    /// <summary>
    /// Создать EfContext в памяти
    /// </summary>
    /// <param name="dbSeeder">Seed тестовых данных</param>
    /// <returns></returns>
    protected EfContext CreateInMemory(Action<EfContext>? dbSeeder = null)
    {
        var options = new DbContextOptionsBuilder<EfContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var context = new EfContext(options);
        dbSeeder?.Invoke(context);
        context.SaveChangesAsync().GetAwaiter().GetResult();

        return context;
    }

    /// <summary>
    /// Добавить песни в бакет пользователя
    /// </summary>
    /// <param name="songs">Песни</param>
    protected void SetUserSongs(List<Song> songs)
        => User.Bucket!.Songs.AddRange(songs);

    /// <summary>
    /// Сконфигурировать логгер
    /// </summary>
    /// <typeparam name="THandler">Тип логгера</typeparam>
    /// <returns>Логгер</returns>
    protected Mock<ILogger<THandler>> ConfigureLogger<THandler>()
        where THandler : class
    {
        var logger = new Mock<ILogger<THandler>>();
        return logger;
    }
    
    /// <inheritdoc />
    public void Dispose() => GC.SuppressFinalize(this);
}