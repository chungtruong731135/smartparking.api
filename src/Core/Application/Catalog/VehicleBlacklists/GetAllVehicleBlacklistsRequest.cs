using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleBlacklists;
public class GetAllVehicleBlacklistsRequest : IRequest<Result<List<VehicleBlacklistDto>>>
{
}
public class GetAllVehicleBlacklistsRequestHandler : IRequestHandler<GetAllVehicleBlacklistsRequest, Result<List<VehicleBlacklistDto>>>
{
    private readonly IRepository<VehicleBlacklist> _repository;
    private readonly IRepository<Branch> _branchRepository;
    public GetAllVehicleBlacklistsRequestHandler(IRepository<VehicleBlacklist> repository, IRepository<Branch> branchRepository)
    {
        _repository = repository;
        _branchRepository = branchRepository;
    }

    public async Task<Result<List<VehicleBlacklistDto>>> Handle(GetAllVehicleBlacklistsRequest request, CancellationToken cancellationToken)
    {
        var blacklists = await _repository.ListAsync(cancellationToken);
        var dtos = blacklists.Select(bl => new VehicleBlacklistDto
        {
            Id = bl.Id,
            PlateNumber = bl.PlateNumber,
            BranchId = bl.BranchId,
            Description = bl.Description
        }).ToList();

        return Result<List<VehicleBlacklistDto>>.Success(dtos);
    }
}

