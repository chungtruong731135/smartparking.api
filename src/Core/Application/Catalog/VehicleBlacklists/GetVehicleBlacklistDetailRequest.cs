namespace TD.WebApi.Application.Catalog.VehicleBlacklists;
public class GetVehicleBlacklistDetailRequest : IRequest<Result<VehicleBlacklistDetailDto>>
{
    public Guid Id { get; set; }
}

public class GetVehicleBlacklistDetailRequestHandler : IRequestHandler<GetVehicleBlacklistDetailRequest, Result<VehicleBlacklistDetailDto>>
{
    private readonly IRepository<VehicleBlacklist> _repository;
    private readonly IRepository<Branch> _branchRepository;

    public GetVehicleBlacklistDetailRequestHandler(IRepository<VehicleBlacklist> repository, IRepository<Branch> branchRepository)
    {
        _repository = repository;
        _branchRepository = branchRepository;
    }

    public async Task<Result<VehicleBlacklistDetailDto>> Handle(GetVehicleBlacklistDetailRequest request, CancellationToken cancellationToken)
    {
        var blacklist = await _repository.GetByIdAsync(request.Id);
        if (blacklist == null)
        {
           throw new NotFoundException("VehicleBlacklist not found.");
        }
        var branch = await _branchRepository.GetByIdAsync(blacklist.BranchId.Value);
        if (branch == null)
        {
            throw new NotFoundException("Branch not found.");
        }


        var dto = new VehicleBlacklistDetailDto
        {
            Id = blacklist.Id,
            PlateNumber = blacklist.PlateNumber,
            BranchId = blacklist.BranchId,
            BranchName = branch.Name,
            Description = blacklist.Description
        };

        return Result<VehicleBlacklistDetailDto>.Success(dto);
    }
}
