using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.BranchUsers;
public class GetBranchUserRequest : IRequest<Result<BranchUserDto>>
{
    public Guid Id { get; set; }

    public GetBranchUserRequest(Guid id) => Id = id;
}

public class BranchUserByIdSpec : Specification<BranchUser, BranchUserDto>
{
    public BranchUserByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetBranchUserRequestHandler : IRequestHandler<GetBranchUserRequest, Result<BranchUserDto>>
{
    private readonly IRepository<BranchUser> _repository;
    private readonly IStringLocalizer _t;

    public GetBranchUserRequestHandler(IRepository<BranchUser> repository, IStringLocalizer<GetBranchUserRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Result<BranchUserDto>> Handle(GetBranchUserRequest request, CancellationToken cancellationToken)
    {
        var item = await _repository.FirstOrDefaultAsync(
            (ISpecification<BranchUser, BranchUserDto>)new BranchUserByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["BranchUser {0} Not Found.", request.Id]);

        return Result<BranchUserDto>.Success(item);
    }
}