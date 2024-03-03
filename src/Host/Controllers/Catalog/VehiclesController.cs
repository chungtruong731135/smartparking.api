using Microsoft.AspNetCore.Mvc;
using TD.WebApi.Application.Catalog.Vehicles;
using TD.WebApi.Application.Catalog.VehicleTypes;

namespace TD.WebApi.Host.Controllers.Catalog;
public class VehiclesController : VersionedApiController
{
    [HttpPost]
    [OpenApiOperation("Create a new vehicle.", "")]
    public async Task<Result<Guid>> CreateAsync(AddVehicleRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a vehicle.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateVehicleRequest request, Guid id)
    {
        if (id != request.Id)
        {
            return BadRequest();
        }

        return Ok(await Mediator.Send(request));
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get details of a vehicle.", "")]
    public Task<Result<VehicleDto>> GetVehicleAsync(Guid id)
    {
        return Mediator.Send(new GetVehicleRequest { VehicleId = id });
    }

    [HttpGet]
    [OpenApiOperation("Get all vehicles.", "")]
    public Task<Result<List<VehicleDto>>> GetAllVehiclesAsync()
    {
        return Mediator.Send(new GetAllVehiclesRequest());
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a vehicle.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteVehicleRequest(id));
    }

}
