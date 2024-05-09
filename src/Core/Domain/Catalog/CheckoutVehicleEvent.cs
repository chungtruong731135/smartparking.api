using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Domain.Catalog;
public class CheckoutVehicleEvent : AuditableEntity, IAggregateRoot
{
    public Guid EventVehicleId { get; set; }
    public DateTime CheckoutDate { get; set; }
    public decimal Amount { get; set; }
    public string? PlateNumber { get; set; }
    public Guid? BranchId { get; set; }
    public virtual EventVehicle EventVehicle { get; set; }
    public virtual Branch? Branch { get; set; }

    public CheckoutVehicleEvent(Guid eventVehicleId, DateTime checkoutDate, decimal amount, string? plateNumber, Guid? branchId)
    {
        EventVehicleId = eventVehicleId;
        CheckoutDate = checkoutDate;
        Amount = amount;
        PlateNumber = plateNumber;
        BranchId = branchId;
    }
}
