using System.Reflection;
using Cassandra;
using RussianSpotify.API.Data.Cassandra.Abstractions;
using RussianSpotify.API.Data.Cassandra.Attributes;

namespace RussianSpotify.API.Data.Cassandra;

public class CassandraContext : IDisposable
{
    private readonly ISession _session;

    public CassandraContext(CassandraContextOptions options)
    {
        var keyspace = options.Keyspace;

        var cluster = Cluster.Builder()
            .AddContactPoint(options.Host)
            .WithPort(options.Port)
            .Build();

        var sysSession = cluster.Connect();

        var createKeyspaceCql = $"CREATE KEYSPACE IF NOT EXISTS {keyspace} " +
                                "WITH replication = { 'class' : 'SimpleStrategy', 'replication_factor' : 1 };";
        sysSession.Execute(createKeyspaceCql);

        _session = cluster.Connect(keyspace);
    }

    public async Task InsertAsync<TEntity>(TEntity entity)
    {
        var tableAttr = typeof(TEntity).GetCustomAttribute<CassandraTableAttribute>() 
                        ?? throw new InvalidOperationException("Missing Table attribute.");
        var props = typeof(TEntity)
            .GetProperties()
            .Where(p => p.IsDefined(typeof(CassandraColumnAttribute)))
            .ToArray();

        var columns = props.Select(p => p.GetCustomAttribute<CassandraColumnAttribute>()!.Name).ToArray();
        var values = props.Select(p => p.GetValue(entity)).ToArray();

        var placeholders = string.Join(", ", columns.Select(_ => "?"));
        var columnsJoined = string.Join(", ", columns);
        var cql = $"INSERT INTO {tableAttr.TableName} ({columnsJoined}) VALUES ({placeholders})";

        var statement = (await _session.PrepareAsync(cql)).Bind(values);
        await _session.ExecuteAsync(statement);
    }

    public async Task RemoveAsync<TEntity>(TEntity entity)
    {
        var tableAttr = typeof(TEntity).GetCustomAttribute<CassandraTableAttribute>() 
                        ?? throw new InvalidOperationException("Missing Table attribute.");

        var idProp = typeof(TEntity).GetProperties()
            .FirstOrDefault(p => p.GetCustomAttribute<CassandraColumnAttribute>()?.Name == "Id");
        if (idProp == null)
            throw new InvalidOperationException("Missing Id column.");

        var idValue = idProp.GetValue(entity);
        if (idValue == null)
            throw new InvalidOperationException("Id value is null.");

        var cql = $"DELETE FROM {tableAttr.TableName} WHERE Id = ?";
        var statement = (await _session.PrepareAsync(cql)).Bind(idValue);
        await _session.ExecuteAsync(statement);
    }

    public async Task<List<TEntity>> FindByAsync<TEntity>(string columnName, object value)
        where TEntity : class, new()
    {
        var tableAttr = typeof(TEntity).GetCustomAttribute<CassandraTableAttribute>()
                        ?? throw new InvalidOperationException("Missing Table attribute.");

        var cql = $"SELECT * FROM {tableAttr.TableName} WHERE {columnName} = ?";
        var statement = (await _session.PrepareAsync(cql)).Bind(value);

        var result = await _session.ExecuteAsync(statement);
        var rows = result.GetRows();

        var props = typeof(TEntity).GetProperties()
            .Where(p => p.IsDefined(typeof(CassandraColumnAttribute)))
            .ToArray();

        var list = new List<TEntity>();
        foreach (var row in rows)
        {
            var entity = new TEntity();
            foreach (var prop in props)
            {
                var columnAttr = prop.GetCustomAttribute<CassandraColumnAttribute>()!;
                var colName = columnAttr.Name;

                if (!row.IsNull(colName))
                {
                    var val = row.GetValue<object>(colName);
                    prop.SetValue(entity, val);
                }
            }
            
            list.Add(entity);
        }

        return list;
    }
    
    public async Task CreateTableIfNotExistsAsync<TEntity>(CancellationToken cancellationToken = default)
        where TEntity : class
    {
        var cassandraTableAttribute = typeof(TEntity).GetCustomAttribute<CassandraTableAttribute>();
        if (cassandraTableAttribute == null)
            throw new InvalidOperationException("The table attribute is missing.");

        var tableName = cassandraTableAttribute.TableName;

        var columns = new List<string>();
        string? primaryKey = null;

        foreach (var property in typeof(TEntity).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var cassandraColumnAttribute = property.GetCustomAttribute<CassandraColumnAttribute>();
            if (cassandraColumnAttribute == null) continue;

            columns.Add($"{cassandraColumnAttribute.Name} {cassandraColumnAttribute.Type}");

            if (cassandraColumnAttribute.Name.Equals("Id", StringComparison.OrdinalIgnoreCase))
            {
                primaryKey = cassandraColumnAttribute.Name;
            }
        }

        if (primaryKey == null)
            throw new InvalidOperationException("No primary key defined in entity.");

        var cql = $"""
                   CREATE TABLE IF NOT EXISTS {tableName} (
                       {string.Join(",\n", columns)},
                       PRIMARY KEY ({primaryKey})
                   );
                   """;

        await _session.ExecuteAsync(new SimpleStatement(cql));
    }
    
    public async Task UpdateAsync<TEntity>(TEntity entity)
        where TEntity : class, IEntityWithId
    {
        var tableAttr = typeof(TEntity).GetCustomAttribute<CassandraTableAttribute>() 
                        ?? throw new InvalidOperationException("Missing Table attribute.");

        var props = typeof(TEntity)
            .GetProperties()
            .Where(p => p.IsDefined(typeof(CassandraColumnAttribute)))
            .ToArray();

        var setProps = props
            .Where(p => !string.Equals(p.GetCustomAttribute<CassandraColumnAttribute>()?.Name, "Id", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        if (!setProps.Any())
            throw new InvalidOperationException("No updatable columns found.");

        var setClauses = string.Join(", ", setProps.Select(p =>
        {
            var columnName = p.GetCustomAttribute<CassandraColumnAttribute>()!.Name;
            return $"{columnName} = ?";
        }));

        var cql = $"UPDATE {tableAttr.TableName} SET {setClauses} WHERE Id = ?";

        var values = setProps.Select(p => p.GetValue(entity)).ToList();
        values.Add(entity.Id); // WHERE Id = ?

        var statement = (await _session.PrepareAsync(cql)).Bind(values.ToArray());
        await _session.ExecuteAsync(statement);
    }

    public void Dispose()
    {
        try
        {
            _session.Dispose();
            GC.SuppressFinalize(this);
        }
        catch (NullReferenceException)
        {
            // Do nothing
        }
    }
}