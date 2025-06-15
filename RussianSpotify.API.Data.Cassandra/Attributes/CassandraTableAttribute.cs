namespace RussianSpotify.API.Data.Cassandra.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CassandraTableAttribute : Attribute
{
    public string TableName { get; set; }
}