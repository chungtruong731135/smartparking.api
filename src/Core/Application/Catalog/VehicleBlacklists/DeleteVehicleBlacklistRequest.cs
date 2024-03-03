using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleBlacklists;
public class DeleteVehicleBlacklistRequest : IRequest<Result<Guid>>
{
    public Guid VehicleBlacklistId { get; set; }

    public DeleteVehicleBlacklistRequest(Guid vehicleBlacklistId)
    {
        VehicleBlacklistId = vehicleBlacklistId;
    }
}

public class DeleteVehicleBlacklistRequestHandler : IRequestHandler<DeleteVehicleBlacklistRequest, Result<Guid>>
{
    private readonly IRepository<VehicleBlacklist> _repository;

    public DeleteVehicleBlacklistRequestHandler(IRepository<VehicleBlacklist> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(DeleteVehicleBlacklistRequest request, CancellationToken cancellationToken)
    {
        var blacklist = await _repository.GetByIdAsync(request.VehicleBlacklistId);
        if (blacklist == null)
        {
            throw new NotFoundException("VehicleBlacklist not found.");
        }

        await _repository.DeleteAsync(blacklist);
        return Result<Guid>.Success(request.VehicleBlacklistId);
    }
}
