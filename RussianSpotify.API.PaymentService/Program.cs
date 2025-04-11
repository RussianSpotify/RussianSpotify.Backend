using MassTransit;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using RussianSpotify.API.PaymentService.Configures;
using RussianSpotify.API.PaymentService.GrpcServices;
using RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;
using RussianSpotify.API.Shared.Data.PostgreSQL.Options;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.Shared.Options;
using RussianSpotify.API.Shared.Options.Kestrel;
using RussianSpotify.API.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Swagger (only for dev)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Migrate DB on startup
await ApplyMigrationsAsync(app.Services);

// Middlewares
app.UseHttpsRedirection();

// Map gRPC endpoints
app.MapGrpcService<PaymentService>();

// Run app
app.Run();


// ==========================
// Local methods
// ==========================

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddGrpc();

    services.AddScoped<SoftDeleteInterceptor>();
    services.AddScoped<UpdateInterceptor>();
    services.AddScoped<IDateTimeProvider, DateTimeProvider>();

    services.AddMassTransitHostedService();
    services.AddMasstransitRabbitMqService(configuration.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>()!);
    services.AddCustomDataContext(configuration.GetSection(nameof(DbContextOptions)).Get<DbContextOptions>()!);

    ConfigureKestrel(builder.WebHost, configuration.GetSection(nameof(KestrelOptions)).Get<KestrelOptions>()!);
}

void ConfigureKestrel(IWebHostBuilder webHostBuilder, KestrelOptions kestrelOptions)
{
    webHostBuilder.UseKestrel(options =>
    {
        var rest = kestrelOptions.Options.First(x => x.EndpointType == EndpointType.Rest);
        var grpc = kestrelOptions.Options.First(x => x.EndpointType == EndpointType.Grpc);

        options.ListenAnyIP(rest.Port, listen => listen.Protocols = HttpProtocols.Http1);
        options.ListenAnyIP(grpc.Port, listen => listen.Protocols = HttpProtocols.Http2);
    });
}

async Task ApplyMigrationsAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();
    await migrator.MigrateAsync(CancellationToken.None);
}