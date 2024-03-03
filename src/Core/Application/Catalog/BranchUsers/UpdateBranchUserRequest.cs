using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WebApi.Application.Catalog.Branches;
using TD.WebApi.Application.Identity.Users;

namespace TD.WebApi.Application.Catalog.BranchUsers;
public class UpdateBranchUserRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? UserId { get; set; }
}

public class UpdateBranchUserRequestValidator : AbstractValidator<UpdateBranchUserRequest>
{
    public UpdateBranchUserRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.BranchId).NotNull().WithMessage("BranchId is required.");
        RuleFor(x => x.UserId).NotNull().WithMessage("UserId is required.");
    }
}

public class UpdateBranchUserRequestHandler : IRequestHandler<UpdateBranchUserRequest, Result<Guid>>
{
    private readonly IRepository<BranchUser> _branchUserRepository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IUserService _userService;

    public UpdateBranchUserRequestHandler(
        IRepository<BranchUser> branchUserRepository,
        IRepository<Branch> branchRepository,
        IUserService userService)
    {
        _branchUserRepository = branchUserRepository;
        _branchRepository = branchRepository;
        _userService = userService;
    }

    public async Task<Result<Guid>> Handle(UpdateBranchUserRequest request, CancellationToken cancellationToken)
    {
        var existingBranchUser = await _branchUserRepository.GetByIdAsync(request.Id, cancellationToken);
        if (existingBranchUser == null)
        {
            throw new NotFoundException("BranchUser with the given ID does not exist.");
        }

        var branchSpec = new BranchByIdSpec(request.BranchId.Value);
        var branchExists = await _branchRepository.FirstOrDefaultAsync(branchSpec, cancellationToken);
        if (branchExists == null)
        {
            throw new NotFoundException("Provided BranchId does not match any existing branch.");
        }

        var user = await _userService.GetAsync(request.UserId.ToString(), cancellationToken);
        if (user == null)
        {
            throw new NotFoundException("Provided UserId does not match any existing user.");
        }

        existingBranchUser.BranchId = request.BranchId;
        existingBranchUser.UserId = request.UserId;
        existingBranchUser.UserName = user.UserName; 

        await _branchUserRepository.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(request.Id);
    }
}

