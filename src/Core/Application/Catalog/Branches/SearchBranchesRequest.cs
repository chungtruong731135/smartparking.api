using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Branches;
public class SearchBranchesRequest : PaginationFilter, IRequest<PaginationResponse<BranchDto>>
{

}

public class BranchesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Branch, BranchDto>
{
    public BranchesBySearchRequestSpec(SearchBranchesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchBranchesRequestHandler : IRequestHandler<SearchBranchesRequest, PaginationResponse<BranchDto>>
{
    private readonly IReadRepository<Branch> _repository;

    public SearchBranchesRequestHandler(IReadRepository<Branch> repository) => _repository = repository;

    public async Task<PaginationResponse<BranchDto>> Handle(SearchBranchesRequest request, CancellationToken cancellationToken)
    {
        var spec = new BranchesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}


