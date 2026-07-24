using AEspejo.FreightQuotes.Domain.Entities;
using AEspejo.FreightQuotes.Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Constant = AEspejo.FreightQuotes.Domain.Entities.Constant;

namespace AEspejo.FreightQuotes.Infrastructure.Persistence;

public class FreightQuotesDbContext(DbContextOptions<FreightQuotesDbContext> options) : DbContext(options)
{
    public DbSet<Accessorial> Accessorials => Set<Accessorial>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<State> States => Set<State>();
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<PartyAddress> PartyAddresses => Set<PartyAddress>();
    public DbSet<Constant> Constants => Set<Constant>();
    public DbSet<ConstantType> ConstantTypes => Set<ConstantType>();
    public DbSet<Carrier> Carriers => Set<Carrier>();
    public DbSet<CarrierSetting> CarrierSettings => Set<CarrierSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<State>()
            .HasOne(s => s.Country)
            .WithMany(c => c.States)
            .HasForeignKey(s => s.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Accessorial>()
            .HasOne(s => s.Type)
            .WithMany()
            .HasForeignKey(s => s.TypeId);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.State)
            .WithMany()
            .HasForeignKey(a => a.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.Country)
            .WithMany()
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PartyAddress>()
            .HasOne(a => a.State)
            .WithMany()
            .HasForeignKey(a => a.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<PartyAddress>()
            .HasOne(a => a.Country)
            .WithMany()
            .HasForeignKey(a => a.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Constant>()
            .HasOne(c => c.ConstantType)
            .WithMany(ct => ct.Constants)
            .HasForeignKey(c => c.ConstantTypeId);

        modelBuilder.Entity<CarrierSetting>()
            .HasOne(cs => cs.Carrier)
            .WithMany(c => c.Settings)
            .HasForeignKey(cs => cs.CarrierId);

        modelBuilder.Entity<CarrierSetting>()
            .HasOne(cs => cs.SettingType)
            .WithMany()
            .HasForeignKey(cs => cs.SettingTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CarrierSetting>()
            .HasOne(cs => cs.CarrierSettingType)
            .WithMany()
            .HasForeignKey(cs => cs.CarrierSettingTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Only one setting per (Carrier, SettingType, Key) combination.
        modelBuilder.Entity<CarrierSetting>()
            .HasIndex(cs => new { cs.CarrierId, cs.SettingTypeId, cs.CarrierSettingTypeId })
            .IsUnique();

        modelBuilder.ApplyConfiguration(new CountrySeed());
        modelBuilder.ApplyConfiguration(new StateSeed());
        modelBuilder.ApplyConfiguration(new ConstantTypeSeed());
        modelBuilder.ApplyConfiguration(new ConstantSeed());        
        modelBuilder.ApplyConfiguration(new AccessorialSeed());
        modelBuilder.ApplyConfiguration(new CarrierSeed());
        modelBuilder.ApplyConfiguration(new CarrierSettingSeed());
    }
}
