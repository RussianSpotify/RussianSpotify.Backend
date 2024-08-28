using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Internal;
using RussianSpotift.API.Data.PostgreSQL;
using RussianSpotift.API.Data.PostgreSQL.Interceptors;
using RussianSpotify.API.Core;
using RussianSpotify.API.Core.Models;
using RussianSpotify.API.WEB.Configurations;
using RussianSpotify.API.WEB.CorsPolicy;
using RussianSpotify.API.WEB.Middlewares;
using RussianSpotify.API.Worker;
using RussianSpotify.Data.S3;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddHangfireWorker();
builder.Services.AddCustomLogging();

// Добавлен медиатр
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Добавлен слой с db контекстом
builder.Services.AddPostgreSQLLayout();
builder.Services.AddCustomDbContext(configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddRedis(configuration);

// Добавлен middleware для обработки исключений
builder.Services
    .AddSingleton<ExceptionMiddleware>()
    .AddSingleton<UpdateInterceptor>()
    .AddSingleton<SoftDeleteInterceptor>();

// Добавлена аутентификация и jwt bearer
builder.Services.AddAuthenticationWithJwtAndExternalServices(configuration);

// Добавлен S3 Storage
builder.Services.AddS3Storage(builder.Configuration.GetSection("MinioS3").Get<MinioOptions>()!);

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

app.MapHealthChecks("/minio-health")
    .RequireCors(CorsPolicyConstants.AllowAll);
app.MapControllers();

app.Run();