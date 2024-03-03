using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WebApi.Application.Common.Models;

namespace TD.WebApi.Application.Catalog.BranchUsers;
public class GetAllBranchUsersRequest : PaginationFilter, IRequest<PaginationResponse<BranchUserDto>>
{

}

public class BranchUsersByPaginationSpec : EntitiesByPaginationFilterSpec<BranchUser, BranchUserDto>
{
    public BranchUsersByPaginationSpec(GetAllBranchUsersRequest request)
        : base(request)
    {
        Query.OrderBy(c => c.UserName, !request.HasOrderBy());
    }
}

public class GetAllBranchUsersRequestHandler : IRequestHandler<GetAllBranchUsersRequest, PaginationResponse<BranchUserDto>>
{
    private readonly IReadRepository<BranchUser> _repository;

    public GetAllBranchUsersRequestHandler(IReadRepository<BranchUser> repository) => _repository = repository;

    public async Task<PaginationResponse<BranchUserDto>> Handle(GetAllBranchUsersRequest request, CancellationToken cancellationToken)
    {
        var spec = new BranchUsersByPaginationSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}
