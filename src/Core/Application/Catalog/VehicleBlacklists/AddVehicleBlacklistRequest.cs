using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleBlacklists;
public class AddVehicleBlacklistRequest : IRequest<Result<Guid>>
{
    public string? PlateNumber { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get; set; }
}

public class AddVehicleBlacklistRequestHandler : IRequestHandler<AddVehicleBlacklistRequest, Result<Guid>>
{
    private readonly IRepository<VehicleBlacklist> _repository;
    private readonly IRepository<Branch> _branchRepository;
    public AddVehicleBlacklistRequestHandler(IRepository<VehicleBlacklist> repository, IRepository<Branch> branchRepository)
    {
        _repository = repository;
        _branchRepository = branchRepository;
    }

    public async Task<Result<Guid>> Handle(AddVehicleBlacklistRequest request, CancellationToken cancellationToken)
    {

        var plateNumberSpec = new VehicleByPlateNumberSpec(request.PlateNumber);
        var existingBlacklist = await _repository.FirstOrDefaultAsync(plateNumberSpec);

        if (existingBlacklist != null)
        {
            throw new NotFoundException("Vehicle with the same plate number already exists in the blacklist.");
        }

        var branch = await _branchRepository.GetByIdAsync(request.BranchId);
        if (branch == null)
        {
            throw new NotFoundException("Branch not found.");
        }

        var blacklist = new VehicleBlacklist(request.PlateNumber, request.BranchId, request.Description);
        await _repository.AddAsync(blacklist);
        return Result<Guid>.Success(blacklist.Id);
    }
}
