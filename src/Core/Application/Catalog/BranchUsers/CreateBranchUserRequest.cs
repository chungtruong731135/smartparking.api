using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WebApi.Application.Catalog.Branches;
using TD.WebApi.Application.Identity.Users;

namespace TD.WebApi.Application.Catalog.BranchUsers;
public class CreateBranchUserRequest : IRequest<Result<Guid>>
{
    public Guid? BranchId { get; set; }
    public Guid? UserId { get; set; }
}

public class CreateBranchUserRequestValidator : AbstractValidator<CreateBranchUserRequest>
{
    public CreateBranchUserRequestValidator()
    {
        RuleFor(x => x.BranchId).NotNull().WithMessage("BranchId is required.");
        RuleFor(x => x.UserId).NotNull().WithMessage("UserId is required.");
    }
}

public class CreateBranchUserRequestHandler : IRequestHandler<CreateBranchUserRequest, Result<Guid>>
{
    private readonly IRepository<BranchUser> _repository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IUserService _userService;

    public CreateBranchUserRequestHandler(IRepository<BranchUser> repository, IUserService userService, IRepository<Branch> branchRepository)
    {
        _repository = repository;
        _userService = userService;
        _branchRepository = branchRepository;
    }

    public async Task<Result<Guid>> Handle(CreateBranchUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetAsync(request.UserId.ToString(), cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("Provided UserId does not match any existing user.");
        }

        var branchSpec = new BranchByIdSpec(request.BranchId.Value);
        var branch = await _branchRepository.FirstOrDefaultAsync(branchSpec, cancellationToken);

        if (branch == null)
        {
            throw new NotFoundException("Provided BranchId does not match any existing branch.");
        }

        var branchUser = new BranchUser(request.BranchId, request.UserId, user.UserName);
        await _repository.AddAsync(branchUser, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(branchUser.Id);
    }
}
