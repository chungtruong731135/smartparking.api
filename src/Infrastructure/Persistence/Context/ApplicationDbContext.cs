using Finbuckle.MultiTenant;
using TD.WebApi.Application.Common.Events;
using TD.WebApi.Application.Common.Interfaces;
using TD.WebApi.Domain.Catalog;
using TD.WebApi.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TD.WebApi.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Branch> Branches => Set<Branch>();
    public DbSet<BranchUser> BranchUsers => Set<BranchUser>();
    public DbSet<EventVehicle> EventVehicles => Set<EventVehicle>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<VehicleBlacklist> VehicleBlacklists { get; set; }
    public DbSet<VehicleMonthlyInvoice> VehicleMonthlyInvoices { get; set; }
    public DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema(SchemaNames.Catalog);
    }
}