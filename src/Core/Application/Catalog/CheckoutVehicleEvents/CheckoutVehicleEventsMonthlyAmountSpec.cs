using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.CheckoutVehicleEvents;
public class CheckoutVehicleEventsMonthlyAmountSpec : Specification<CheckoutVehicleEvent>
{
    public CheckoutVehicleEventsMonthlyAmountSpec(CalculateMonthlyTotalAmountRequest request)
    {
        var toDate = DateTime.UtcNow;
        var fromDate = toDate.AddMonths(-5);

        if (request.BranchId.HasValue)
        {
            Query.Where(e => e.BranchId == request.BranchId.Value);
        }

        Query.Where(e => e.CheckoutDate >= fromDate && e.CheckoutDate <= toDate);
    }
}
