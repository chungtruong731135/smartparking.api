using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Vehicles;
public class VehicleDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Owner { get; set; }
    public string? PlateNumber { get; set; }
    public string? VehicleImage { get; set; }
    public string? PlateImage { get; set; }
    public Guid? VehicleTypeId { get; set; }
    public Guid? TicketId { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateStop { get; set; }
    public DateTime? DateExtend { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get; set; }
}

