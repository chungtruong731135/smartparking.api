using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleTypes;
public class GetVehicleTypeRequest : IRequest<Result<VehicleTypeDto>>
{
    public Guid VehicleTypeId { get; set; }
}
public class GetVehicleTypeRequestHandler : IRequestHandler<GetVehicleTypeRequest, Result<VehicleTypeDto>>
{
    private readonly IRepository<VehicleType> _vehicleTypeRepository;
    public GetVehicleTypeRequestHandler(IRepository<VehicleType> vehicleTypeRepository)
    {
        _vehicleTypeRepository = vehicleTypeRepository;
    }

    public async Task<Result<VehicleTypeDto>> Handle(GetVehicleTypeRequest request, CancellationToken cancellationToken)
    {
        var vehicleType = await _vehicleTypeRepository.GetByIdAsync(request.VehicleTypeId);
        if (vehicleType == null)
        {
            throw new NotFoundException("VehicleType not found.");
        }

        var dto = new VehicleTypeDto
        {
            Id = vehicleType.Id,
            Name = vehicleType.Name,
            Type = vehicleType.Type,
            TinhTienVeThang = vehicleType.TinhTienVeThang,
            DonGiaThang = vehicleType.DonGiaThang,
            PhiCapLoaiThe = vehicleType.PhiCapLoaiThe,
            PhiLamMoiThe = vehicleType.PhiLamMoiThe,
            DonGiaLuot = vehicleType.DonGiaLuot,
            DonGiaBlock = vehicleType.DonGiaBlock,
            ThoiGianMienPhi = vehicleType.ThoiGianMienPhi,
            ThoiGianDongGia = vehicleType.ThoiGianDongGia,
            ThoiGianBlock = vehicleType.ThoiGianBlock,
            LoaiTinhTienVeLuot = vehicleType.LoaiTinhTienVeLuot,
            BranchId = vehicleType.BranchId,
            Description = vehicleType.Description
        };

        return Result<VehicleTypeDto>.Success(dto);
    }
}
