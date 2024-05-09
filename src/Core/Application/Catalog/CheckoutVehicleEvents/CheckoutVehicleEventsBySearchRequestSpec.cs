using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.CheckoutVehicleEvents;
public class CheckoutVehicleEventsBySearchRequestSpec : EntitiesByPaginationFilterSpec<CheckoutVehicleEvent, CheckoutVehicleEventDto>, ISingleResultSpecification
{
    public CheckoutVehicleEventsBySearchRequestSpec(SearchCheckoutVehicleEventsRequest request)
        : base(request)
    {
        Query.OrderBy(e => e.CheckoutDate, !request.HasOrderBy())
            .Where(e => !request.BranchId.HasValue || e.BranchId == request.BranchId.Value);

        if (!string.IsNullOrEmpty(request.PlateNumber))
        {
            Query.Where(e => e.PlateNumber.ToLower().Contains(request.PlateNumber.ToLower()));
        }

        if (request.FromDate.HasValue)
        {
            Query.Where(e => e.CheckoutDate >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            Query.Where(e => e.CheckoutDate <= request.ToDate.Value);
        }
    }
}
