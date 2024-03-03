namespace TD.WebApi.Domain.Catalog;

/// <summary>
/// Phương tiện thuê bao tháng
/// </summary>
public class Vehicle : AuditableEntity, IAggregateRoot
{
    public string Name { get;  set; }
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// Chu so huu
    /// </summary>
    public string? Owner { get; set; }
    public string? PlateNumber { get; set; }
    public string? VehicleImage { get; set; }
    public string? PlateImage { get; set; }
    /// <summary>
    /// Loai phuong tien
    /// </summary>
    public Guid? VehicleTypeId { get; set; }
    /// <summary>
    /// Ve xe
    /// </summary>
    public Guid? TicketId { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateStop { get; set; }
    public DateTime? DateExtend { get; set; }

    public Guid? BranchId { get; set; }
   
    public string? Description { get;  set; }

    public virtual Branch? Branch { get; set; }
    public virtual VehicleType? VehicleType { get; set; }
    public virtual Ticket? Ticket { get; set; }

    public Vehicle(string name, string? phoneNumber, string? owner, string? plateNumber, string? vehicleImage, string? plateImage, DefaultIdType? vehicleTypeId, DefaultIdType? ticketId, DateTime? dateStart, DateTime? dateStop, DateTime? dateExtend, DefaultIdType? branchId, string? description)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Owner = owner;
        PlateNumber = plateNumber;
        VehicleImage = vehicleImage;
        PlateImage = plateImage;
        VehicleTypeId = vehicleTypeId;
        TicketId = ticketId;
        DateStart = dateStart;
        DateStop = dateStop;
        DateExtend = dateExtend;
        BranchId = branchId;
        Description = description;
    }

    public Vehicle Update(string name, string? phoneNumber, string? owner, string? plateNumber, string? vehicleImage, string? plateImage, DefaultIdType? vehicleTypeId, DefaultIdType? ticketId, DateTime? dateStart, DateTime? dateStop, DateTime? dateExtend, DefaultIdType? branchId, string? description)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Owner = owner;
        PlateNumber = plateNumber;
        VehicleImage = vehicleImage;
        PlateImage = plateImage;
        VehicleTypeId = vehicleTypeId;
        TicketId = ticketId;
        DateStart = dateStart;
        DateStop = dateStop;
        DateExtend = dateExtend;
        BranchId = branchId;
        Description = description;
        return this;
    }
}