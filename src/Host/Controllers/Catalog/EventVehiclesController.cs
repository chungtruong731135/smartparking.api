using MediatR;
using TD.WebApi.Application.Catalog.EventVehicles;

namespace TD.WebApi.Host.Controllers.Catalog;

public class EventVehiclesController : VersionedApiController
{
    [HttpPost("search-event-vehicles")]
    [OpenApiOperation("Search event vehicles by branch and lane direction.", "")]
    public Task<PaginationResponse<EventVehicleDto>> SearchAsync(SearchEventVehiclesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("enter")]
    [OpenApiOperation("Enter a vehicle to the parking.", "")]
    public Task<Result<Guid>> EnterVehicle(EnterVehicleRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("exit")]
    [OpenApiOperation("Exit a vehicle from the parking.", "")]
    public Task<Result<Guid>> ExitVehicle(ExitVehicleRequest request)
    {
        return Mediator.Send(request);
    }

    //[HttpGet("all-vehicles-in")]
    //[OpenApiOperation("Get all vehicles that are currently in the parking.", "")]
    //public Task<Result<List<EventVehicleDto>>> GetAllVehiclesInSession()
    //{
    //    return Mediator.Send(new GetAllVehicleEnterSessionRequest());
    //}

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get event vehicle details.", "")]
    public Task<Result<EventVehicleDto>> GetEventVehicleAsync(Guid id)
    {
        return Mediator.Send(new GetEventVehicleRequest(id));
    }

}
