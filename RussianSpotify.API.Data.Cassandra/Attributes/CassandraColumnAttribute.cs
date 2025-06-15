namespace RussianSpotify.API.Data.Cassandra.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class CassandraColumnAttribute : Attribute
{
    public string Name { get; set; }
    
    public string Type { get; set; }
}