using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.CheckoutVehicleEvents;
public class CheckoutVehicleEventsAmountSpec : Specification<CheckoutVehicleEvent, CheckoutVehicleEventDto>, ISingleResultSpecification
{
    public CheckoutVehicleEventsAmountSpec(CalculateTotalAmountRequest request)
    {
        if (request.BranchId.HasValue)
        {
            Query.Where(e => e.BranchId == request.BranchId.Value);
        }

        if (request.FromDate.HasValue)
        {
            Query.Where(e => e.CheckoutDate >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            // Adjust ToDate to include the entire day
            var toDateEnd = request.ToDate.Value.Date.AddDays(1);
            Query.Where(e => e.CheckoutDate <= toDateEnd);
        }
    }
}
