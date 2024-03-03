namespace TD.WebApi.Domain.Catalog;

/// <summary>
/// Danh sách Event quẹt thẻ xe
/// </summary>
public class EventVehicle : AuditableEntity, IAggregateRoot
{
    public string? PlateNumber { get; set; }
    public string? DetectedPlateNumber { get; set; }
    public DateTime? DateTimeEvent { get; set; }
    public string? PlateImage { get; set; }
    public string? VehicleImage { get; set; }

    /// <summary>
    /// Làn ra hoặc vào
    /// IN: Vào
    /// OUT: Ra
    /// </summary>
    public string LaneDirection { get; set; }

    /// <summary>
    /// Id phần cứng
    /// </summary>
    public string? HardwareSyncId { get; set; }

    /// <summary>
    /// Thông tin nhân viên cho xe vào bãi
    /// </summary>
    public string? UserName { get; set; }
    public Guid? UserId { get; set; }
    public Guid? TicketId { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get;  set; }
    public int? Status { get; set; }

    public virtual Ticket? Ticket { get; set; }
    public virtual Branch? Branch { get; set; }

    public EventVehicle(string? plateNumber, string? detectedPlateNumber, DateTime? dateTimeEvent, string? plateImage, string? vehicleImage, string laneDirection, string? hardwareSyncId, string? userName, DefaultIdType? userId, DefaultIdType? ticketId, DefaultIdType? branchId, string? description, int? status)
    {
        PlateNumber = plateNumber;
        DetectedPlateNumber = detectedPlateNumber;
        DateTimeEvent = dateTimeEvent;
        PlateImage = plateImage;
        VehicleImage = vehicleImage;
        LaneDirection = laneDirection;
        HardwareSyncId = hardwareSyncId;
        UserName = userName;
        UserId = userId;
        TicketId = ticketId;
        BranchId = branchId;
        Description = description;
        Status = status;
    }

    public EventVehicle Update(string? plateNumber, string? detectedPlateNumber, DateTime? dateTimeEvent, string? plateImage, string? vehicleImage, string laneDirection, string? hardwareSyncId, string? userName, DefaultIdType? userId, DefaultIdType? ticketId, DefaultIdType? branchId, string? description, int? status)
    {
        PlateNumber = plateNumber;
        DetectedPlateNumber = detectedPlateNumber;
        DateTimeEvent = dateTimeEvent;
        PlateImage = plateImage;
        VehicleImage = vehicleImage;
        LaneDirection = laneDirection;
        HardwareSyncId = hardwareSyncId;
        UserName = userName;
        UserId = userId;
        TicketId = ticketId;
        BranchId = branchId;
        Description = description;
        Status = status;
        return this;
    }
}