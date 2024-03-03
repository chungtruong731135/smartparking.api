using TD.WebApi.Application.Common.Persistence;
using TD.WebApi.Domain.Catalog;

namespace TD.WebApi.Application.Catalog.Vehicles;
public class DeleteVehicleRequest : IRequest<Result<Guid>>
{
    public Guid VehicleId { get; set; }
    public DeleteVehicleRequest(Guid vehicleId)
    {
        VehicleId = vehicleId;
    }
}
public class DeleteVehicleRequestHandler : IRequestHandler<DeleteVehicleRequest, Result<Guid>>
{
    private readonly IRepository<Vehicle> _vehicleRepository;

    public DeleteVehicleRequestHandler(IRepository<Vehicle> vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<Result<Guid>> Handle(DeleteVehicleRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
            if (vehicle == null)
            {
                throw new NotFoundException("Wtf?.");
            }
            await _vehicleRepository.DeleteAsync(vehicle);
            return Result<Guid>.Success(request.VehicleId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

