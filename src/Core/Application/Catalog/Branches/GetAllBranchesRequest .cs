using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Branches;
public class GetAllBranchesRequest : IRequest<Result<List<BranchDto>>>
{

}
public class GetAllBranchesRequestHandler : IRequestHandler<GetAllBranchesRequest, Result<List<BranchDto>>>
{
    private readonly IReadRepository<Branch> _repository;

    public GetAllBranchesRequestHandler(IReadRepository<Branch> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<BranchDto>>> Handle(GetAllBranchesRequest request, CancellationToken cancellationToken)
    {
        var branches = await _repository.ListAsync(cancellationToken);
        var branchDtos = branches.Select(b => new BranchDto
        {
            Id = b.Id,
            Name = b.Name,
            PhoneNumber = b.PhoneNumber,
            Email = b.Email,
            Website = b.Website,
            Address = b.Address,
            Logo = b.Logo,
            Description = b.Description
        }).ToList();

        return Result<List<BranchDto>>.Success(branchDtos);
    }
}
