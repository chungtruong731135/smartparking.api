using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.BranchUsers;
public class SearchBranchUsersRequest : PaginationFilter, IRequest<PaginationResponse<BranchUserDto>>
{
    public Guid? UserId { get; set; }
}

public class SearchBranchUsersRequestHandler : IRequestHandler<SearchBranchUsersRequest, PaginationResponse<BranchUserDto>>
{
    private readonly IReadRepository<BranchUser> _repository;

    public SearchBranchUsersRequestHandler(IReadRepository<BranchUser> repository) => _repository = repository;

    public async Task<PaginationResponse<BranchUserDto>> Handle(SearchBranchUsersRequest request, CancellationToken cancellationToken)
    {
        var spec = new BranchUsersByUserIdSpec(request.UserId);
        return await _repository.PaginatedListAsync<BranchUser, BranchUserDto>(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}
