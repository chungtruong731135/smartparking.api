using Finbuckle.MultiTenant.EntityFrameworkCore;
using TD.WebApi.Domain.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TD.WebApi.Infrastructure.Persistence.Configuration;

public class BrandConfig : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(256);
    }
}

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);

        builder
            .Property(p => p.ImagePath)
                .HasMaxLength(2048);
    }
}

public class BranchConfig : IEntityTypeConfiguration<Branch>
{
    public void Configure(EntityTypeBuilder<Branch> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(256);

        builder
            .Property(b => b.PhoneNumber)
                .HasMaxLength(20);

        builder
            .Property(b => b.Email)
                .HasMaxLength(100);

        builder
            .Property(b => b.Website)
                .HasMaxLength(2048);

        builder
            .Property(b => b.Address)
                .HasMaxLength(512);

        builder
            .Property(b => b.Logo)
                .HasMaxLength(2048);

        builder
            .Property(b => b.Description)
                .HasMaxLength(1024);
    }
}

public class BranchUserConfig : IEntityTypeConfiguration<BranchUser>
{
    public void Configure(EntityTypeBuilder<BranchUser> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(b => b.UserName)
                .HasMaxLength(256);
    }
}

public class EventVehicleConfig : IEntityTypeConfiguration<EventVehicle>
{
    public void Configure(EntityTypeBuilder<EventVehicle> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(e => e.PlateNumber)
                .HasMaxLength(50);

        builder
            .Property(e => e.DetectedPlateNumber)
                .HasMaxLength(50);

        builder
            .Property(e => e.PlateImage)
                .HasMaxLength(2048);

        builder
            .Property(e => e.VehicleImage)
                .HasMaxLength(2048);

        builder
            .Property(e => e.LaneDirection)
                .IsRequired()
                .HasMaxLength(10);

        builder
            .Property(e => e.HardwareSyncId)
                .HasMaxLength(256);

        builder
            .Property(e => e.Description)
                .HasMaxLength(1000);
    }
}

public class TicketConfig : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(t => t.Name)
                .HasMaxLength(256);

        builder
            .Property(t => t.CardNumber)
                .HasMaxLength(50);

        builder
            .Property(t => t.Type)
                .HasMaxLength(20);

        builder
            .Property(t => t.LockedNote)
                .HasMaxLength(500);

        builder
            .Property(t => t.LoseNote)
                .HasMaxLength(500);

        builder
            .Property(t => t.Description)
                .HasMaxLength(1000);
    }
}

public class VehicleConfig : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(256);

        builder
            .Property(v => v.PhoneNumber)
                .HasMaxLength(50);

        builder
            .Property(v => v.Owner)
                .HasMaxLength(256);

        builder
            .Property(v => v.PlateNumber)
                .HasMaxLength(50);

        builder
            .Property(v => v.VehicleImage)
                .HasMaxLength(2048);

        builder
            .Property(v => v.PlateImage)
                .HasMaxLength(2048);

        builder
            .Property(v => v.Description)
                .HasMaxLength(1000);
    }
}

public class VehicleBlacklistConfig : IEntityTypeConfiguration<VehicleBlacklist>
{
    public void Configure(EntityTypeBuilder<VehicleBlacklist> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(vb => vb.PlateNumber)
                .HasMaxLength(50);

        builder
            .Property(vb => vb.Description)
                .HasMaxLength(1000);
    }
}

public class VehicleMonthlyInvoiceConfig : IEntityTypeConfiguration<VehicleMonthlyInvoice>
{
    public void Configure(EntityTypeBuilder<VehicleMonthlyInvoice> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(vmi => vmi.Description)
                .HasMaxLength(1000);
    }
}

public class VehicleTypeConfig : IEntityTypeConfiguration<VehicleType>
{
    public void Configure(EntityTypeBuilder<VehicleType> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(vt => vt.Name)
                .IsRequired()
                .HasMaxLength(256);

        builder
            .Property(vt => vt.Description)
                .HasMaxLength(1000);
    }
}

public class CheckoutVehicleEventConfig : IEntityTypeConfiguration<CheckoutVehicleEvent>
{
    public void Configure(EntityTypeBuilder<CheckoutVehicleEvent> builder)
    {
        builder.IsMultiTenant();

        builder
            .Property(ve => ve.EventVehicleId)
                .IsRequired();

        builder
            .Property(ve => ve.CheckoutDate)
                .IsRequired();

        builder
            .Property(ve => ve.Amount)
                .IsRequired()
                .HasColumnType("decimal(18, 2)");

        builder
            .Property(ve => ve.PlateNumber)
                .HasMaxLength(50);

        builder
            .Property(ve => ve.BranchId)
                .IsRequired();
    }
}