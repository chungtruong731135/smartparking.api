using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleBlacklists;
public class UpdateVehicleBlacklistRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string? PlateNumber { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get; set; }
}

public class UpdateVehicleBlacklistRequestHandler : IRequestHandler<UpdateVehicleBlacklistRequest, Result<Guid>>
{
    private readonly IRepository<VehicleBlacklist> _repository;
    private readonly IRepository<Branch> _branchRepository;

    public UpdateVehicleBlacklistRequestHandler(IRepository<VehicleBlacklist> repository, IRepository<Branch> branchRepository)
    {
        _repository = repository;
        _branchRepository = branchRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateVehicleBlacklistRequest request, CancellationToken cancellationToken)
    {
        var blacklist = await _repository.GetByIdAsync(request.Id);
        if (blacklist == null) throw new NotFoundException("Vehicle not found in the blacklist.");

        var branch = await _branchRepository.GetByIdAsync(request.BranchId);
        if (branch == null)
        {
            throw new NotFoundException("Branch not found.");
        }

        blacklist.Update(request.PlateNumber, request.BranchId, request.Description);
        await _repository.UpdateAsync(blacklist);
        return Result<Guid>.Success(blacklist.Id);
    }
}
