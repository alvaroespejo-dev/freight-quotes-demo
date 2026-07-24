using AEspejo.FreightQuotes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AEspejo.FreightQuotes.Api.Extensions;

public static class DatabaseServiceCollectionExtensions
{
    private const string SqlServerMigrationsAssembly = "AEspejo.FreightQuotes.Migrations.SqlServer";
    private const string SqliteMigrationsAssembly = "AEspejo.FreightQuotes.Migrations.Sqlite";

    /// <summary>
    /// Registers the <see cref="FreightQuotesDbContext"/> against the provider named by the
    /// "DatabaseProvider" setting ("SqlServer" or "Sqlite"). Migrations are not portable
    /// between providers, so each one points at its own migrations assembly.
    /// </summary>
    public static IServiceCollection AddFreightQuotesDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var provider = configuration["DatabaseProvider"] ?? "Sqlite";
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<FreightQuotesDbContext>(opt =>
        {
            switch (provider.ToLowerInvariant())
            {
                case "sqlite":
                    opt.UseSqlite(connectionString,
                        b => b.MigrationsAssembly(SqliteMigrationsAssembly));
                    break;

                case "sqlserver":
                    opt.UseSqlServer(connectionString,
                        b => b.MigrationsAssembly(SqlServerMigrationsAssembly));
                    break;

                default:
                    throw new InvalidOperationException(
                        $"DatabaseProvider '{provider}' is not supported. Use 'SqlServer' or 'Sqlite'.");
            }
        });

        return services;
    }
}
