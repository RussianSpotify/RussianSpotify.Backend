using RussianSpotift.API.Data.PostgreSQL;
using RussianSpotift.API.Data.PostgreSQL.Interceptors;
using RussianSpotify.API.ChatMessageSaver.Configurations;
using RussianSpotify.API.Core;
using RussianSpotify.API.Shared.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// RabbitMQ
builder.Services.AddRabbitMq(builder.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>()!);

builder.Services.AddPostgreSQLLayout();
builder.Services.AddCustomDbContext(builder.Configuration.GetConnectionString("DefaultConnection")!);

builder.Services
    .AddSingleton<UpdateInterceptor>()
    .AddSingleton<SoftDeleteInterceptor>();

builder.Services.AddCoreLayout();

var app = builder.Build();

app.Run();