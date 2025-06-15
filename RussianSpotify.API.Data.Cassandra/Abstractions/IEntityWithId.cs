namespace RussianSpotify.API.Data.Cassandra.Abstractions;

public interface IEntityWithId
{
    string Id { get; set; }
}