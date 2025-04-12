using Microsoft.AspNetCore.Builder;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.SubscriptionDispatcher;
using RussianSpotify.API.Shared.Options;
using RussianSpotify.API.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Добавляем Hangfire
builder.Services.AddHangfireWorker();
builder.Services.AddScoped<IEmailSender, EmailSender>();

// 3. Настройки HangfireOptions (из appsettings.json например)
var hangfireOptions = builder.Configuration.GetSection(nameof(HangfireOptions)).Get<HangfireOptions>() ?? new HangfireOptions();

var app = builder.Build();

// 4. Включаем Dashboard и сервер Hangfire
app.UseHangfireWorker(hangfireOptions);

app.Run();