using AEspejo.FreightQuotes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AEspejo.FreightQuotes.Migrations.SqlServer;

/// <summary>
/// Lets "dotnet ef" build the model for this provider without a startup project.
/// The connection string is only used to pick the provider - no database is contacted
/// when adding or scripting migrations.
/// </summary>
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<FreightQuotesDbContext>
{
    public FreightQuotesDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<FreightQuotesDbContext>()
            .UseSqlServer(
                "Server=.\\LOCALHOST;Database=FreightQuotesNew;Integrated Security=True;TrustServerCertificate=True;",
                b => b.MigrationsAssembly(typeof(DesignTimeDbContextFactory).Assembly.GetName().Name))
            .Options;

        return new FreightQuotesDbContext(options);
    }
}
