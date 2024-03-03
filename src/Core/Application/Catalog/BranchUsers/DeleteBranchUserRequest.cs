using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.BranchUsers;
public class DeleteBranchUserRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }

    public DeleteBranchUserRequest(Guid id) => Id = id;
}

public class DeleteBranchUserRequestHandler : IRequestHandler<DeleteBranchUserRequest, Result<Guid>>
{
    private readonly IRepository<BranchUser> _repository;
    private readonly IStringLocalizer _t;

    public DeleteBranchUserRequestHandler(IRepository<BranchUser> repository, IStringLocalizer<DeleteBranchUserRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Result<Guid>> Handle(DeleteBranchUserRequest request, CancellationToken cancellationToken)
    {
        var existingItem = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (existingItem == null)
        {
            throw new NotFoundException($"BranchUser with ID {request.Id} not found.");
        }

        await _repository.DeleteAsync(existingItem, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}

