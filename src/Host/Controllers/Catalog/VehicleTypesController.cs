using Microsoft.AspNetCore.Mvc;
using TD.WebApi.Application.Catalog.Brands;
using TD.WebApi.Application.Catalog.Tickets;
using TD.WebApi.Application.Catalog.VehicleTypes;

namespace TD.WebApi.Host.Controllers.Catalog;
public class VehicleTypesController : VersionedApiController
{
    [HttpPost("search")]
    [OpenApiOperation("Search tickets using available filters.", "")]
    public Task<PaginationResponse<TicketDto>> SearchAsync(SearchTicketsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost]
    [OpenApiOperation("Create a new vehicle type.", "")]
    public Task<Result<Guid>> CreateAsync(CreateVehicleTypeRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet]
    [OpenApiOperation("Get all vehicle types.", "")]
    public Task<Result<List<VehicleTypeDto>>> GetAllAsync()
    {
        return Mediator.Send(new GetAllVehicleTypesRequest());
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get vehicle type details.", "")]
    public Task<Result<VehicleTypeDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetVehicleTypeRequest { VehicleTypeId = id });
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a vehicle type.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateVehicleTypeRequest request, Guid id)
    {
        if (id != request.Id)
            return BadRequest();

        return Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a vehicle type.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteVehicleTypeRequest(id));
    }

}
