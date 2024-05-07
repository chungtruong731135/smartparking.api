using MediatR;
using Microsoft.AspNetCore.Mvc;
using TD.WebApi.Domain.Catalog;
using TD.WebApi.Application.Catalog.Tickets;

namespace TD.WebApi.Host.Controllers.Catalog;

public class TicketsController : VersionedApiController
{
    [HttpPost("search")]
    [OpenApiOperation("Search tickets using available filters.", "")]
    public Task<PaginationResponse<TicketDto>> SearchAsync(SearchTicketsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet]
    [OpenApiOperation("Get all tickets.", "")]
    public Task<Result<List<TicketDto>>> GetAllAsync()
    {
        return Mediator.Send(new GetAllTicketsRequest());
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get ticket details.", "")]
    public Task<Result<TicketDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetTicketRequest(id));
    }

    [HttpPost]
    [OpenApiOperation("Create a new ticket.", "")]
    public Task<Result<Guid>> CreateAsync(CreateTicketRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("{id:guid}")]
    [OpenApiOperation("Update a ticket.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateTicketRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a ticket.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteTicketRequest(id));
    }
}
