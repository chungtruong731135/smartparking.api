using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.EventVehicles;
public class SearchEventVehiclesRequest : PaginationFilter, IRequest<PaginationResponse<EventVehicleDto>>
{
    public Guid? BranchId { get; set; }
    public string LaneDirection { get; set; }
    public string? PlateNumber { get; set; }
    public Guid? TicketId { get; set; }
}

public class SearchEventVehiclesRequestHandler : IRequestHandler<SearchEventVehiclesRequest, PaginationResponse<EventVehicleDto>>
{
    private readonly IReadRepository<EventVehicle> _repository;

    public SearchEventVehiclesRequestHandler(IReadRepository<EventVehicle> repository) => _repository = repository;

    public async Task<PaginationResponse<EventVehicleDto>> Handle(SearchEventVehiclesRequest request, CancellationToken cancellationToken)
    {
        var spec = new EventVehiclesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}
