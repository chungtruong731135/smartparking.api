using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleBlacklists;
public class SearchVehicleBlacklistsRequest : PaginationFilter, IRequest<PaginationResponse<VehicleBlacklistDto>>
{
    public Guid? BranchId { get; set; }
}

public class SearchVehicleBlacklistsRequestHandler : IRequestHandler<SearchVehicleBlacklistsRequest, PaginationResponse<VehicleBlacklistDto>>
{
    private readonly IReadRepository<VehicleBlacklist> _repository;

    public SearchVehicleBlacklistsRequestHandler(IReadRepository<VehicleBlacklist> repository) => _repository = repository;

    public async Task<PaginationResponse<VehicleBlacklistDto>> Handle(SearchVehicleBlacklistsRequest request, CancellationToken cancellationToken)
    {
        var spec = new VehicleBlacklistsByBranchIdSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}
