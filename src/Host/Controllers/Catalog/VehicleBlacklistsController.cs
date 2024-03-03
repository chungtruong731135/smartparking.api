using Microsoft.AspNetCore.Mvc;
using TD.WebApi.Application.Catalog.VehicleBlacklists;

namespace TD.WebApi.Host.Controllers.Catalog;
public class VehicleBlacklistsController : VersionedApiController
{
    [HttpPost("search")]
    [OpenApiOperation("Search vehicle blacklists using the branch ID.", "")]
    public Task<PaginationResponse<VehicleBlacklistDto>> SearchAsync(SearchVehicleBlacklistsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost]
    [OpenApiOperation("Add a new vehicle to the blacklist.", "")]
    public Task<Result<Guid>> AddToBlacklist(AddVehicleBlacklistRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a vehicle in the blacklist.", "")]
    public Task<Result<Guid>> UpdateBlacklist(UpdateVehicleBlacklistRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a vehicle from the blacklist.", "")]
    public Task<Result<Guid>> DeleteFromBlacklist(Guid id)
    {
        return Mediator.Send(new DeleteVehicleBlacklistRequest(id));
    }

    [HttpGet]
    [OpenApiOperation("Get all vehicles in the blacklist.", "")]
    public Task<Result<List<VehicleBlacklistDto>>> GetAllBlacklists()
    {
        return Mediator.Send(new GetAllVehicleBlacklistsRequest());
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get details of a vehicle in the blacklist.", "")]
    public Task<Result<VehicleBlacklistDetailDto>> GetBlacklistDetail(Guid id)
    {
        return Mediator.Send(new GetVehicleBlacklistDetailRequest { Id = id });
    }

}
