using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleBlacklists;
public class VehicleBlacklistsByBranchIdSpec : EntitiesByPaginationFilterSpec<VehicleBlacklist, VehicleBlacklistDto>
{
    public VehicleBlacklistsByBranchIdSpec(SearchVehicleBlacklistsRequest request)
        : base(request)
    {
        Query
            .Where(v => v.BranchId.Equals(request.BranchId!.Value), request.BranchId.HasValue);

        if (!string.IsNullOrEmpty(request.PlateNumber))
        {
            Query.Where(e => e.PlateNumber.ToLower().Contains(request.PlateNumber.ToLower()));
        }
    }
}