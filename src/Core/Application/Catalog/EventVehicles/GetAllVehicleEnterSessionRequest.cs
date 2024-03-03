using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.EventVehicles;
public class GetAllVehicleEnterSessionRequest : IRequest<Result<List<EventVehicleDto>>>
{
}

//public class GetAllVehicleEnterSessionRequestHandler : IRequestHandler<GetAllVehicleEnterSessionRequest, Result<List<EventVehicleDto>>>
//{
//    private readonly IRepository<EventVehicle> _repository;

//    public GetAllVehicleEnterSessionRequestHandler(IRepository<EventVehicle> repository)
//    {
//        _repository = repository;
//    }

//    public async Task<Result<List<EventVehicleDto>>> Handle(GetAllVehicleEnterSessionRequest request, CancellationToken cancellationToken)
//    {
//        var vehicles = await _repository.ListAsync(cancellationToken);

//        var vehiclesIn = vehicles
//            .Where(v => v.LaneDirection == "IN")
//            .Select(v => new EventVehicleDto
//            {
//                Id = v.Id,
//                PlateNumber = v.PlateNumber,
//                DetectedPlateNumber = v.DetectedPlateNumber,
//                DateTimeEvent = v.DateTimeEvent,
//                PlateImage = v.PlateImage,
//                VehicleImage = v.VehicleImage,
//                LaneDirection = v.LaneDirection,
//                HardwareSyncId = v.HardwareSyncId,
//                UserName = v.UserName,
//                UserId = v.UserId,
//                TicketId = v.TicketId,
//                BranchId = v.BranchId,
//                Description = v.Description,
//                Status = v.Status
//            })
//            .ToList();

//        return Result<List<EventVehicleDto>>.Success(vehiclesIn);
//    }
//}
