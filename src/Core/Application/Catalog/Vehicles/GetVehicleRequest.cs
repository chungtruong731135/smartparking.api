using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Vehicles;
public class GetVehicleRequest : IRequest<Result<VehicleDto>>
{
    public Guid VehicleId { get; set; }

}

public class GetVehicleRequestHandler : IRequestHandler<GetVehicleRequest, Result<VehicleDto>>
{
    private readonly IRepository<Vehicle> _vehicleRepository;

    public GetVehicleRequestHandler(IRepository<Vehicle> vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<Result<VehicleDto>> Handle(GetVehicleRequest request, CancellationToken cancellationToken)
    {
        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
        if (vehicle == null)
        {
            throw new NotFoundException("Vehicle not found.");
        }

        var dto = new VehicleDto
        {
            Id = vehicle.Id,
            Name = vehicle.Name,
            PhoneNumber = vehicle.PhoneNumber,
            Owner = vehicle.Owner,
            PlateNumber = vehicle.PlateNumber,
            VehicleImage = vehicle.VehicleImage,
            PlateImage = vehicle.PlateImage,
            VehicleTypeId = vehicle.VehicleTypeId,
            TicketId = vehicle.TicketId,
            DateStart = vehicle.DateStart,
            DateStop = vehicle.DateStop,
            DateExtend = vehicle.DateExtend,
            BranchId = vehicle.BranchId,
            Description = vehicle.Description
        };

        return Result<VehicleDto>.Success(dto);
    }
}

