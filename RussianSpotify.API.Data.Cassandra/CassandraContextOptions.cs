namespace RussianSpotify.API.Data.Cassandra;

public class CassandraContextOptions
{
    public string Host { get; init; } = "127.0.0.1";
    public int Port { get; init; } = 9042;
    public string Keyspace { get; init; } = "keyspace_base";
}