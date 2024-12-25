using Microsoft.AspNetCore.Server.Kestrel.Core;
using RussianSpotify.API.Files.Data;
using RussianSpotify.API.Files.Grpc;
using RussianSpotify.API.Files.Interfaces;
using RussianSpotify.API.Files.Options;
using RussianSpotify.API.Files.Services;
using RussianSpotify.API.Files.Services.S3Service;
using RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;
using RussianSpotify.API.Shared.Extensions.ConfigurationExtensions;
using RussianSpotify.API.Shared.Extensions.ConfigurationExtensions.CorsPolicy;
using RussianSpotify.API.Shared.Interfaces;
using RussianSpotify.API.Shared.Middlewares;
using RussianSpotify.API.Shared.Options.Kestrel;
using RussianSpotify.API.Shared.Services;
using RussianSpotify.API.WEB.Configurations;
using DbContextOptions = RussianSpotify.API.Shared.Data.PostgreSQL.Options.DbContextOptions;

var builder = WebApplication.CreateBuilder(args);

// TODO: Навести порядок(разбить по регионам, что-то завести в отдельные Extension-ы)
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddSwaggerGenWithAuth(typeof(Program).Assembly);
}

builder.Services.AddAuthenticationWithJwtAndExternalServices(builder.Configuration);

builder.Services
    .AddDataContext(builder.Configuration.GetSection(nameof(DbContextOptions)).Get<DbContextOptions>()!);

builder.Services
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddResponseCompression();
builder.Services.AddCustomLogging();
builder.Services.AddHttpContextAccessor();
builder.Services
    .AddScoped<IUserContext, UserContext>()
    .AddScoped<IFileHelper, FileHelper>();

// Добавлен middleware для обработки исключений
builder.Services
    .AddSingleton<ExceptionMiddleware>()
    .AddSingleton<UpdateInterceptor>()
    .AddSingleton<SoftDeleteInterceptor>();

var kestrelOptions = builder.Configuration.GetSection(nameof(KestrelOptions)).Get<KestrelOptions>()!;
builder.WebHost.UseKestrel(options =>
{
    var restOptions = kestrelOptions.Options.First(x => x.EndpointType == EndpointType.Rest);
    options.ListenAnyIP(restOptions.Port, listenOptions => { listenOptions.Protocols = HttpProtocols.Http1; });

    var grpcOptions = kestrelOptions.Options.First(x => x.EndpointType == EndpointType.Grpc);
    options.ListenAnyIP(grpcOptions.Port, listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
});

builder.Services.AddGrpc();

builder.Services.AddS3Storage(builder.Configuration.GetSection("MinioS3").Get<MinioOptions>()!);

var app = builder.Build();

#region Migrations

var migrator = app.Services.CreateScope().ServiceProvider.GetRequiredService<IMigrator>();
await migrator.MigrateAsync(CancellationToken.None);

#endregion

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks("/minio-health")
    .RequireCors(CorsPolicyConstants.AllowAll);

app.MapControllers();
app.MapGrpcService<FileServer>();

app.Run();