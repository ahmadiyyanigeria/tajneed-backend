using Microsoft.Extensions.Configuration;
using Npgsql;

namespace TajneedApi.Infrastructure.Extensions;

public static class NpgsqlConnectionExtension
{
    public static NpgsqlConnectionStringBuilder GetDbConnectionStringBuilder(this IConfiguration configuration)
    {
        return new NpgsqlConnectionStringBuilder
        {
            Host = configuration.GetValue<string>("DatabaseSettings:Host"),
            Database = configuration.GetValue<string>("DatabaseSettings:Name"),
            Password = configuration.GetValue<string>("DatabaseSettings:Password"),
            Username = configuration.GetValue<string>("DatabaseSettings:Username"),
            IncludeErrorDetail = true,
            Pooling = true,
            IntegratedSecurity = true,
            Port = configuration.GetValue<int?>("DatabaseSettings:Port") ?? 5432
        };
    }
}