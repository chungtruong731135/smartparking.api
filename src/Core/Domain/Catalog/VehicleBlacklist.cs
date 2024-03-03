namespace TD.WebApi.Domain.Catalog;

/// <summary>
/// Danh sách xe bị blacklist
/// </summary>
public class VehicleBlacklist : AuditableEntity, IAggregateRoot
{
    public string? PlateNumber { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get;  set; }
    public virtual Branch? Branch { get; set; }

    public VehicleBlacklist(string? plateNumber, DefaultIdType? branchId, string? description)
    {
        PlateNumber = plateNumber;
        BranchId = branchId;
        Description = description;
    }

    public VehicleBlacklist Update(string? plateNumber, DefaultIdType? branchId, string? description)
    {
        PlateNumber = plateNumber;
        BranchId = branchId;
        Description = description;
        return this;
    }
}