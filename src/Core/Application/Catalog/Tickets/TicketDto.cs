using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Tickets;
public class TicketDto :IDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? CardNumber { get; set; }
    public string? Type { get; set; }
    public bool? IsActive { get; set; }
    public bool? IsLocked { get; set; }
    public DateTime? LockedDate { get; set; }
    public string? LockedNote { get; set; }
    public bool? IsLose { get; set; }
    public DateTime? LoseDate { get; set; }
    public string? LoseNote { get; set; }
    public string? Description { get; set; }
    public Guid? VehicleTypeId { get; set; }
    public Guid? BranchId { get; set; }

}
