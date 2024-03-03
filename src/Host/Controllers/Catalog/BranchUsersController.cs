using TD.WebApi.Application.Catalog.BranchUsers;

namespace TD.WebApi.Host.Controllers.Catalog;

public class BranchUsersController : VersionedApiController
{
    [HttpPost("search")]
    [OpenApiOperation("Search branch users using UserId.", "")]
    public Task<PaginationResponse<BranchUserDto>> SearchAsync(SearchBranchUsersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet]
    [OpenApiOperation("Get all branch users.", "")]
    public Task<PaginationResponse<BranchUserDto>> GetAllAsync()
    {
        return Mediator.Send(new GetAllBranchUsersRequest());
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get branch user details.", "")]
    public Task<Result<BranchUserDto>> GetAsync(Guid id)
    {
        return Mediator.Send(new GetBranchUserRequest(id));
    }


    [HttpPost]
    [OpenApiOperation("Create a branch user.", "")]
    public Task<Result<Guid>> CreateAsync(CreateBranchUserRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a branch user.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateBranchUserRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a branch user.", "")]
    public Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteBranchUserRequest(id));
    }

}
