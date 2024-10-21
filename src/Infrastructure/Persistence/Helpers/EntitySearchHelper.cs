using Npgsql;
using System.Text;

namespace TajneedApi.Infrastructure.Persistence.Helpers;

public class EntitySearchHelper<T>(DbContext context)
    where T : class
{
    private readonly DbContext _context = context;

    public IQueryable<T> SearchEntity(string keyword)
    {
        var entityType = _context.Model.FindEntityType(typeof(T));
        var existingColumns = entityType.GetProperties().Select(p => p.GetColumnName()).ToList();
        var columnToPropertyMap = entityType.GetProperties().ToDictionary(p => p.GetColumnName(), p => p.Name);

        var stringColumns = existingColumns.Where(col =>
        {
            if (columnToPropertyMap.TryGetValue(col, out var propertyName))
            {
                var property = entityType.FindProperty(propertyName);
                return property != null &&
                       (property.ClrType == typeof(string) ||
                        property.GetColumnType() == "jsonb");
            }
            return false;
        });

        var searchTermLower = keyword.ToLower();
        var queryBuilder = new StringBuilder();
        queryBuilder.Append($"SELECT * FROM {entityType.GetTableName()} WHERE ");

        foreach (var column in stringColumns)
        {
            if (columnToPropertyMap.TryGetValue(column, out var propertyName) &&
                entityType.FindProperty(propertyName)?.GetColumnType() == "jsonb")
            {
                queryBuilder.Append($"LOWER({column}::text) COLLATE \"C\" ILIKE '%' || @searchKeyword || '%' OR ");
            }
            else
            {
                queryBuilder.Append($"LOWER({column}) COLLATE \"C\" ILIKE '%' || @searchKeyword || '%' OR ");
            }
        }

        queryBuilder.Remove(queryBuilder.Length - 4, 4); // Remove the last " OR "

        return _context.Set<T>().FromSqlRaw(queryBuilder.ToString(), new NpgsqlParameter("@searchKeyword", searchTermLower));
    }
}