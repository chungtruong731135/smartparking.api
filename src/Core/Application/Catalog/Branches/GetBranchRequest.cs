using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Branches;
public class GetBranchRequest : IRequest<Result<BranchDto>>
{
    public Guid Id { get; set; }

    public GetBranchRequest(Guid id) => Id = id;
}

public class GetBranchRequestHandler : IRequestHandler<GetBranchRequest, Result<BranchDto>>
{
    private readonly IRepository<Branch> _repository;
    private readonly IStringLocalizer _t;

    public GetBranchRequestHandler(IRepository<Branch> repository, IStringLocalizer<GetBranchRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<Result<BranchDto>> Handle(GetBranchRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.FirstOrDefaultAsync(
            (ISpecification<Branch, BranchDto>)new BranchByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Branch {0} Not Found.", request.Id]);

        return Result<BranchDto>.Success(item);
    }
}