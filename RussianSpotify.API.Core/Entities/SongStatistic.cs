using RussianSpotify.API.Data.Cassandra.Abstractions;
using RussianSpotify.API.Data.Cassandra.Attributes;
using RussianSpotify.API.Data.Cassandra.Utils;

namespace RussianSpotify.API.Core.Entities;

[CassandraTable(TableName = nameof(SongStatistic))]
public class SongStatistic : IEntityWithId
{
    [CassandraColumn(Name = nameof(Id), Type = CassandraTypes.String)]
    public string Id { get; set; }
    
    [CassandraColumn(Name = nameof(PlaysNumber), Type = CassandraTypes.Int32)]
    public int PlaysNumber { get; set; }
}