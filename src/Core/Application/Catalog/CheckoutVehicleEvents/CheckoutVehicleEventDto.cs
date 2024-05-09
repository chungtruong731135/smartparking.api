using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.CheckoutVehicleEvents;
public class CheckoutVehicleEventDto : IDto
{
    public Guid Id { get; set; }
    public Guid? EventVehicleId { get; set; }
    public DateTime? CheckoutDate { get; set; }
    public decimal? Amount { get; set; }
    public string? PlateNumber { get; set; }
    public Guid? BranchId { get; set; }
}
