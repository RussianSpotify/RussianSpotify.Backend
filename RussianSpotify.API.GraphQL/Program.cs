using RussianSpotift.API.Data.PostgreSQL;
using RussianSpotify.API.Core.Abstractions;
using RussianSpotify.API.GraphQL.Gql.Main;
using RussianSpotify.API.GraphQL.Gql.Subscription;
using RussianSpotify.API.GraphQL.GqlTypes;
using RussianSpotify.API.Shared.Data.PostgreSQL.Extensions;
using RussianSpotify.API.Shared.Data.PostgreSQL.Interceptors;
using RussianSpotify.API.Shared.Middlewares;
using SubscriptionDbContext = RussianSpotify.Grpc.SubscriptionService.Data.SubscriptionDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRouting()
    .AddGraphQLServer(disableDefaultSecurity: true)
    .AddQueryType(m => m.Name("Query"))
    .AddType<AccountQuery>()
    .AddType<AuthorQuery>()
    .AddType<PlaylistQuery>()
    .AddType<SongQuery>()
    .AddType<SubscriptionQuery>()
    .BindRuntimeType<uint, UnsignedIntType>()
    .AddFiltering(x =>
        x.AddDefaults().BindRuntimeType<uint, UnsignedIntOperationFilterInputType>())
    .AddSorting()
    .AddProjections();

// Добавлен middleware для обработки исключений
builder.Services
    .AddSingleton<ExceptionMiddleware>()
    .AddSingleton<UpdateInterceptor>()
    .AddSingleton<SoftDeleteInterceptor>();

builder.Services.AddCustomDbContext<IDbContext, EfContext>(
    builder.Configuration.GetSection("DataContext:MainConnectionString").Get<string>()!);
builder.Services.AddCustomDbContext<RussianSpotify.Grpc.SubscriptionService.Data.IDbContext, SubscriptionDbContext>(
    builder.Configuration.GetSection("DataContext:SubscriptionConnectionString").Get<string>()!);

builder.WebHost.UseUrls("http://0.0.0.0:56807");

var app = builder.Build();

app.UseRouting()
    .UseEndpoints(endpoints => endpoints.MapGraphQL());

app.Run();