using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleTypes;
public class DeleteVehicleTypeRequest : IRequest<Result<Guid>>
{
    public Guid VehicleTypeId { get; set; }

    public DeleteVehicleTypeRequest(Guid vehicleTypeId)
    {
        VehicleTypeId = vehicleTypeId;
    }
}
public class DeleteVehicleTypeRequestHandler : IRequestHandler<DeleteVehicleTypeRequest, Result<Guid>>
{
    private readonly IRepository<VehicleType> _vehicleTypeRepository;

    public DeleteVehicleTypeRequestHandler(IRepository<VehicleType> vehicleTypeRepository)
    {
        _vehicleTypeRepository = vehicleTypeRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteVehicleTypeRequest request, CancellationToken cancellationToken)
    {
        var vehicleType = await _vehicleTypeRepository.GetByIdAsync(request.VehicleTypeId);

        if (vehicleType == null)
        {
            throw new NotFoundException("VehicleType not found.");
        }

        await _vehicleTypeRepository.DeleteAsync(vehicleType);

        return Result<Guid>.Success(request.VehicleTypeId);
    }
}
