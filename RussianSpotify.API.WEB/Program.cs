#region

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Internal;
using RussianSpotift.API.Data.PostgreSQL;
using RussianSpotify.API.Client;
using RussianSpotify.API.Core;
using RussianSpotify.API.Core.Models;
using RussianSpotify.API.Core.Services;
using RussianSpotify.API.Grpc.Options;
using RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;
using RussianSpotify.API.Shared.Extensions.ConfigurationExtensions;
using RussianSpotify.API.Shared.Extensions.ConfigurationExtensions.CorsPolicy;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.Shared.Middlewares;
using RussianSpotify.API.Shared.Options;
using RussianSpotify.API.Shared.Services;
using RussianSpotify.API.WEB.Configurations;
using RussianSpotify.API.Worker;

#endregion

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGenWithAuth(typeof(Program).Assembly);
}

builder.Services.AddHangfireWorker();
builder.Services.AddCustomLogging();

// Добавлен медиатр
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Добавлен слой с db контекстом
// TODO: Как будто бы это должно быть в одном методе AddPostgreSQLLayout и AddCustomDbContext
builder.Services.AddPostgreSqlLayout();
builder.Services.AddCustomDbContext(configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddExternalSubscriptionDbContext(configuration["ExternalSubscriptionDbContext"]!);
builder.Services.AddSignalR();
var grpcOptions = builder.Configuration.GetSection(nameof(GrpcOptions)).Get<GrpcOptions>()!;
builder.Services.AddGrpcServices(grpcOptions);

// Redis
var redisOptions = builder.Configuration.GetSection(nameof(RedisOptions)).Get<RedisOptions>()!;
builder.Services.AddRedis(redisOptions);

// RabbitMQ
builder.Services.AddRabbitMq(configuration.GetSection("RabbitMq").Get<RabbitMqOptions>()!);

// Добавлен middleware для обработки исключений
builder.Services
    .AddSingleton<ExceptionMiddleware>()
    .AddSingleton<UpdateInterceptor>()
    .AddSingleton<SoftDeleteInterceptor>();

builder.Services.AddGoogleService(configuration.GetSection("GoogleService").Get<HttpApiClientOptions>()!);
builder.Services.AddScoped<IFileControllerHelper, FileControllerHelper>();

// Добавлена аутентификация и jwt bearer
builder.Services.AddAuthenticationWithJwtAndExternalServices(configuration);

// Response Compression 
builder.Services.AddResponseCompression();

// Настройка CORS
builder.Services.AddCustomCors();

// Добавлен слой Core
builder.Services.AddCoreLayout();
builder.Services.AddDistributedMemoryCache(options =>
{
    options.Clock = new SystemClock(); // Устанавливаем часы, используемые для временных меток
    options.ExpirationScanFrequency = TimeSpan.FromHours(2); // Частота сканирования для проверки просроченных записей
});

builder.Services.Configure<MemoryCacheOptions>(options =>
{
    options.ExpirationScanFrequency = TimeSpan.FromHours(2); // Частота сканирования для проверки просроченных записей
    options.SizeLimit = 1000; // Максимальное количество элементов в кэше
});

builder.WebHost.UseUrls("http://0.0.0.0:80");

var app = builder.Build();

// Применение миграций
using var scoped = app.Services.CreateScope();
var migrator = scoped.ServiceProvider.GetRequiredService<Migrator>();
await migrator.MigrateAsync();

app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Добавлено использование middleware для обработки исключений
app.UseMiddleware<ExceptionMiddleware>();
app.UseHangfireWorker(builder.Configuration.GetSection("Hangfire").Get<HangfireOptions>()!);

// Настройка CORS
app.UseCors(CorsPolicyConstants.AllowAll);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chat-hub");

app.Run();