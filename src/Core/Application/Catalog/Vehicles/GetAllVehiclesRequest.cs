using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WebApi.Application.Catalog.VehicleTypes;

namespace TD.WebApi.Application.Catalog.Vehicles;
public class GetAllVehiclesRequest : IRequest<Result<List<VehicleDto>>>
{
}
public class GetAllVehiclesRequestHandler : IRequestHandler<GetAllVehiclesRequest, Result<List<VehicleDto>>>
{
    private readonly IRepository<Vehicle> _vehicleRepository;

    public GetAllVehiclesRequestHandler(IRepository<Vehicle> vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<Result<List<VehicleDto>>> Handle(GetAllVehiclesRequest request, CancellationToken cancellationToken)
    {
        var vehicles = await _vehicleRepository.ListAsync(cancellationToken);
        var vehicleDtos = vehicles.Select(v => new VehicleDto
        {
            Id = v.Id,
            Name = v.Name,
            PhoneNumber = v.PhoneNumber,
            Owner = v.Owner,
            PlateNumber = v.PlateNumber,
            VehicleImage = v.VehicleImage,
            PlateImage = v.PlateImage,
            VehicleTypeId = v.VehicleTypeId,
            TicketId = v.TicketId,
            DateStart = v.DateStart,
            DateStop = v.DateStop,
            DateExtend = v.DateExtend,
            BranchId = v.BranchId,
            Description = v.Description
        }).ToList();

        return Result<List<VehicleDto>>.Success(vehicleDtos);
    }
}
