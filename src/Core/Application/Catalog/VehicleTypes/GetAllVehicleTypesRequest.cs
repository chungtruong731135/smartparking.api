using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.WebApi.Application.Catalog.Tickets;
using TD.WebApi.Domain.Catalog;

namespace TD.WebApi.Application.Catalog.VehicleTypes;
public class GetAllVehicleTypesRequest : IRequest<Result<List<VehicleTypeDto>>>
{
}

public class GetAllVehicleTypesRequestHandler : IRequestHandler<GetAllVehicleTypesRequest, Result<List<VehicleTypeDto>>>
{
    private readonly IRepository<VehicleType> _vehicleTypeRepository;

    public GetAllVehicleTypesRequestHandler(IRepository<VehicleType> vehicleTypeRepository)
    {
        _vehicleTypeRepository = vehicleTypeRepository;
    }

    public async Task<Result<List<VehicleTypeDto>>> Handle(GetAllVehicleTypesRequest request, CancellationToken cancellationToken)
    {
        var vehicleTypes = await _vehicleTypeRepository.ListAsync(cancellationToken);
        var vehicleTypeDtos = vehicleTypes.Select(t => new VehicleTypeDto
        {
            Id = t.Id,
            Name = t.Name,
            Type = t.Type,
            TinhTienVeThang = t.TinhTienVeThang,
            DonGiaThang = t.DonGiaThang,
            PhiCapLoaiThe = t.PhiCapLoaiThe,
            PhiLamMoiThe = t.PhiLamMoiThe,
            DonGiaLuot = t.DonGiaLuot,
            DonGiaBlock = t.DonGiaBlock,
            ThoiGianMienPhi = t.ThoiGianMienPhi,
            ThoiGianDongGia = t.ThoiGianDongGia,
            ThoiGianBlock = t.ThoiGianBlock,
            LoaiTinhTienVeLuot = t.LoaiTinhTienVeLuot,
            BranchId = t.BranchId,
            Description = t.Description
        }).ToList();

        return Result<List<VehicleTypeDto>>.Success(vehicleTypeDtos);
    }
}