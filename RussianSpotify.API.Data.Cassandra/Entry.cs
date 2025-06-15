using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RussianSpotify.API.Data.Cassandra;

public static class Entry
{
    public static IServiceCollection AddCassandraLayout(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Entry).Assembly));
        
        var host = configuration["Cassandra:Host"]!;
        var port = int.Parse(configuration["Cassandra:Port"]!);
        var keyspace = configuration["Cassandra:Keyspace"]!;

        services.AddScoped<CassandraContext>(_ => new CassandraContext(new CassandraContextOptions
        {
            Host = host,
            Port = port,
            Keyspace = keyspace,
        }));
        
        return services;
    }
}