namespace AEspejo.FreightQuotes.Infrastructure.Persistence.Seeds;

/// <summary>
/// Shared values used by the <see cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{TEntity}"/> seeds.
/// </summary>
internal static class SeedDefaults
{
    /// <summary>
    /// Creation timestamp stamped on every seeded row.
    /// The kind must be <see cref="DateTimeKind.Utc"/>: Npgsql maps <see cref="DateTime"/> to
    /// "timestamp with time zone" and rejects any other kind.
    /// </summary>
    public static readonly DateTime CreatedUTC = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}
