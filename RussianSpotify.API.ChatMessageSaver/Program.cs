using RussianSpotift.API.Data.PostgreSQL;
using RussianSpotify.API.ChatMessageSaver.Configurations;
using RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;
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

var app = builder.Build();

app.Run();