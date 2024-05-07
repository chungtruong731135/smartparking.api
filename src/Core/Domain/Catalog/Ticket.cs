namespace TD.WebApi.Domain.Catalog;


/// <summary>
/// Thẻ xe
/// </summary>
public class Ticket : AuditableEntity, IAggregateRoot
{
    public string? Name { get; set; }
    public string? CardNumber { get; set; }
    /// <summary>
    /// Loại thẻ xe
    /// 0: Vé lượt
    /// 1: Vé thuê bao
    /// </summary>
    public string? Type { get; set; }

    public bool? IsActive { get; set; } = false;
    public bool? IsLocked { get; set; } = false;
    public DateTime? LockedDate { get; set; }
    public string? LockedNote { get; set; }
    public bool? IsLose { get; set; }
    public DateTime? LoseDate { get; set; }
    public string? LoseNote { get; set; }
      
    public string? Description { get; set; }
    public Guid? VehicleTypeId { get; set; }
    public Guid? BranchId { get; set; }

    public virtual VehicleType? VehicleType { get; set; }
    public virtual Branch? Branch { get; set; }

    public Ticket(string? name, string? cardNumber, string? type, bool? isActive, bool? isLocked, DateTime? lockedDate, string? lockedNote, bool? isLose, DateTime? loseDate, string? loseNote, string? description, DefaultIdType? vehicleTypeId, DefaultIdType? branchId)
    {
        Name = name;
        CardNumber = cardNumber;
        Type = type;
        IsActive = isActive;
        IsLocked = isLocked;
        LockedDate = lockedDate;
        LockedNote = lockedNote;
        IsLose = isLose;
        LoseDate = loseDate;
        LoseNote = loseNote;
        Description = description;
        VehicleTypeId = vehicleTypeId;
        BranchId = branchId;
    }

    public Ticket Update(string? name, string? cardNumber, string? type, bool? isActive, bool? isLocked, DateTime? lockedDate, string? lockedNote, bool? isLose, DateTime? loseDate, string? loseNote, string? description, DefaultIdType? vehicleTypeId, DefaultIdType? branchId)
    {
        Name = name;
        CardNumber = cardNumber;
        Type = type;
        IsActive = isActive;
        IsLocked = isLocked;
        LockedDate = lockedDate;
        LockedNote = lockedNote;
        IsLose = isLose;
        LoseDate = loseDate;
        LoseNote = loseNote;
        Description = description;
        VehicleTypeId = vehicleTypeId;
        BranchId = branchId;
        return this;
    }
}