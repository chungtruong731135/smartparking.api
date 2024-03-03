using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleBlacklists;
public class VehicleBlacklistDetailDto :IDto
{
    public Guid Id { get; set; }
    public string? PlateNumber { get; set; }
    public Guid? BranchId { get; set; }
    public string? BranchName { get; set; } 
    public string? Description { get; set; }
}

