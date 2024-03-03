using TD.WebApi.Application.Catalog.Branches;

namespace TD.WebApi.Host.Controllers.Catalog;

public class BranchesController : VersionedApiController
{
    [HttpPost("search")]
    [OpenApiOperation("Search branches using available filters.", "")]
    public Task<PaginationResponse<BranchDto>> SearchAsync(SearchBranchesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet]
    [OpenApiOperation("Get all branches.", "")]
    public Task<Result<List<BranchDto>>> GetAllAsync()
    {
        return Mediator.Send(new GetAllBranchesRequest());
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get branch details.", "")]
    public Task<Result<BranchDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBranchRequest(id));
    }

    [HttpPost]
    [OpenApiOperation("Create a new branch.", "")]
    public Task<Result<Guid>> CreateAsync(CreateBranchRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a branch.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBranchRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a brand.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBranchRequest(id));
    }
}
