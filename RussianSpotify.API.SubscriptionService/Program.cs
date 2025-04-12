using MassTransit;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;
using RussianSpotify.API.Shared.Data.PostgreSQL.Options;
using RussianSpotify.API.Shared.Extensions.ConfigurationExtensions;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.Shared.Middlewares;
using RussianSpotify.API.Shared.Options;
using RussianSpotify.API.Shared.Options.Kestrel;
using RussianSpotify.API.Shared.Services;
using RussianSpotify.Grpc.SubscriptionService.Configures;
using RussianSpotify.Grpc.SubscriptionService.Features.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
configuration.AddEnvironmentVariables();

// Add services
ConfigureServices(builder.Services, configuration, builder.Environment);
ConfigureKestrel(builder.WebHost, configuration.GetSection(nameof(KestrelOptions)).Get<KestrelOptions>()!);

var app = builder.Build();

// DB migration
await ApplyMigrationsAsync(app.Services);

// Middleware pipeline
ConfigureMiddleware(app);

app.Run();


// ===========================
// Local methods
// ===========================
void ConfigureServices(IServiceCollection services, IConfiguration config, IWebHostEnvironment env)
{
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    if (env.IsDevelopment())
        services.AddSwaggerGenWithAuth(typeof(Program).Assembly);

    services.AddGrpc();

    services.AddResponseCompression();
    services.AddCors(setup =>
    {
        setup.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });

    // Dependency Injection
    services.AddScoped<IDateTimeProvider, DateTimeProvider>();
    services.AddScoped<SoftDeleteInterceptor>();
    services.AddScoped<UpdateInterceptor>();
    services.AddScoped<ISubscribeService, SubscribeService>();
    services.AddTransient<ExceptionMiddleware>();
    services.AddScoped<IUserContext, UserContext>();
    services.AddHttpContextAccessor();

    services.AddDataContext(config.GetSection(nameof(DbContextOptions)).Get<DbContextOptions>()!);
    services.AddMassTransitConfigure(config.GetSection(nameof(RabbitMqOptions)).Get<RabbitMqOptions>()!);
    services.AddMassTransitHostedService();

    services.AddHostedService<OutBoxDispatcherBackgroundService>();
    services.AddAuthenticationWithJwtAndExternalServices(config);
}

void ConfigureKestrel(IWebHostBuilder webHost, KestrelOptions options)
{
    webHost.ConfigureKestrel(kestrel =>
    {
        var rest = options.Options.First(x => x.EndpointType == EndpointType.Rest);
        var grpc = options.Options.First(x => x.EndpointType == EndpointType.Grpc);

        kestrel.ListenAnyIP(rest.Port, lo => lo.Protocols = HttpProtocols.Http1);
        kestrel.ListenAnyIP(grpc.Port, lo => lo.Protocols = HttpProtocols.Http2);
    });
}

async Task ApplyMigrationsAsync(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();
    await migrator.MigrateAsync(CancellationToken.None);
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseResponseCompression();
    app.UseCors();

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
}
