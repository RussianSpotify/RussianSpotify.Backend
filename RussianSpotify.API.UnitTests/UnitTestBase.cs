using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using RussianSpotift.API.Data.PostgreSQL;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.Core.DefaultSettings;
using RussianSpotify.API.Core.Entities;
using RussianSpotify.API.Core.Requests.Playlist.PutPlaylist;
using File = RussianSpotify.API.Core.Entities.File;
using ILogger = Castle.Core.Logging.ILogger;

namespace RussianSpotify.API.UnitTests;

/// <summary>
/// Базовая конфигурация для тестов
/// </summary>
public class UnitTestBase : IDisposable
{
    private const string CurrentUserId = "53afbb05-bb2d-45e0-8bef-489ef1cd6fdc"; 
    
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
    ///  Помощник по работе с файлами
    /// </summary>
    protected Mock<IFileHelper> FileHelper { get; set; }
    
    /// <summary>
    /// Менеджер по пользователю
    /// </summary>
    protected Mock<UserManager<User>> UserManager { get; set; }
    
    
    /// <summary>
    /// Конструктор
    /// </summary>
    protected UnitTestBase()
    {
        DateTimeProvider = new Mock<IDateTimeProvider>();
        DateTimeProvider.Setup(x => x.CurrentDate)
            .Returns(new DateTime(2004, 12, 12));

        JwtGenerator = new Mock<IJwtGenerator>();
        JwtGenerator.Setup(x => x.GenerateToken(It.IsAny<List<Claim>>()))
            .Returns(Guid.NewGuid().ToString);

        UserContext = new Mock<IUserContext>();
        UserContext.Setup(x => x.CurrentUserId)
            .Returns(Guid.Parse(CurrentUserId));

        SubscriptionService = new Mock<ISubscriptionHandler>();
        SubscriptionService.Setup(x => x.GetSubscription(It.IsAny<Guid>()))
            .Verifiable();
        SubscriptionService.Setup(x => x.Subscribe(It.IsAny<Guid>(), It.IsAny<int>()))
            .Verifiable();

        FileHelper = new Mock<IFileHelper>();
        FileHelper.Setup(x => x.IsImage(It.IsAny<File>()))
            .Returns(true);
        FileHelper.Setup(
            x => x.DeleteFileAsync(
                It.IsAny<File>(),
                It.IsAny<CancellationToken>()))
            .Returns(() => Task.CompletedTask);
        
        var userStoreMock = new Mock<IUserStore<User>>();
        var optionsMock = new Mock<IOptions<IdentityOptions>>();
        var passwordHasherMock = new Mock<IPasswordHasher<User>>();
        var userValidators = new List<IUserValidator<User>>();
        var passwordValidators = new List<IPasswordValidator<User>>();
        var lookupNormalizerMock = new Mock<ILookupNormalizer>();
        var identityErrorDescriberMock = new Mock<IdentityErrorDescriber>();
        var serviceProviderMock = new Mock<IServiceProvider>();
        var loggerMock = new Mock<ILogger<UserManager<User>>>();


        UserManager = new Mock<UserManager<User>>(
            userStoreMock.Object,
            optionsMock.Object,
            passwordHasherMock.Object,
            userValidators,
            passwordValidators,
            lookupNormalizerMock.Object,
            identityErrorDescriberMock.Object,
            serviceProviderMock.Object,
            loggerMock.Object
        );

        UserManager.Setup(x => x.GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(new List<string>
            {
                BaseRoles.UserRoleName,
                BaseRoles.AuthorRoleName,
                BaseRoles.AdminRoleName,
            });
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