using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Branches;
public class DeleteBranchRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteBranchRequest(Guid id) => Id = id;
}

public class DeleteBranchRequestHandler : IRequestHandler<DeleteBranchRequest, Result<Guid>>
{
    private readonly IRepository<Branch> _repository;
    private readonly IStringLocalizer _t;

    public DeleteBranchRequestHandler(IRepository<Branch> repository, IStringLocalizer<DeleteBranchRequestHandler> localizer)
    {
        _repository = repository;
        _t = localizer;
    }

    public async Task<Result<Guid>> Handle(DeleteBranchRequest request, CancellationToken cancellationToken)
    {
        var branch = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (branch == null)
        {
            throw new NotFoundException(_t["Branch with ID {0} not found.", request.Id]);
        }

        await _repository.DeleteAsync(branch, cancellationToken);
        return Result<Guid>.Success(request.Id);
    }
}

