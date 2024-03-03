using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.EventVehicles;
public class EventVehicleDto : IDto
{
    public Guid Id { get; set; }
    public string? PlateNumber { get; set; }
    public string? DetectedPlateNumber { get; set; }
    public DateTime? DateTimeEvent { get; set; }
    public string? PlateImage { get; set; }
    public string? VehicleImage { get; set; }
    public string LaneDirection { get; set; }
    public string? HardwareSyncId { get; set; }
    public string? UserName { get; set; }
    public Guid? UserId { get; set; }
    public Guid? TicketId { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get; set; }
    public int? Status { get; set; }
}

