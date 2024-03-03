namespace TD.WebApi.Application.Catalog.EventVehicles;

public class EventVehiclesBySearchRequestSpec : EntitiesByPaginationFilterSpec<EventVehicle, EventVehicleDto>
{
    public EventVehiclesBySearchRequestSpec(SearchEventVehiclesRequest request)
        : base(request)
    {
        Query
            .OrderBy(ev => ev.DateTimeEvent, !request.HasOrderBy())
            .Where(ev => ev.BranchId.Equals(request.BranchId!.Value), request.BranchId.HasValue);

        if (!string.IsNullOrEmpty(request.LaneDirection))
        {
            Query.Where(ev => ev.LaneDirection.Contains(request.LaneDirection));
        }

        if (!string.IsNullOrEmpty(request.PlateNumber))
        {
            Query.Where(ev => ev.PlateNumber.ToLower().Contains(request.PlateNumber.ToLower()));
        }
    }
}
