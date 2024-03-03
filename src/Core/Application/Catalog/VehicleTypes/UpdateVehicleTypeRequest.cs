using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleTypes;
public class UpdateVehicleTypeRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Type { get; set; }
    public int? TinhTienVeThang { get; set; }
    public int? DonGiaThang { get; set; }
    public int? PhiCapLoaiThe { get; set; }
    public int? PhiLamMoiThe { get; set; }
    public int? DonGiaLuot { get; set; }
    public int? DonGiaBlock { get; set; }
    public int? ThoiGianMienPhi { get; set; }
    public int? ThoiGianDongGia { get; set; }
    public int? ThoiGianBlock { get; set; }
    public int? LoaiTinhTienVeLuot { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get; set; }
}

public class UpdateVehicleTypeRequestValidator : AbstractValidator<UpdateVehicleTypeRequest>
{
    public UpdateVehicleTypeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.Type)
            .NotNull().WithMessage("Type is required.");
    }
}

public class UpdateVehicleTypeRequestHandler : IRequestHandler<UpdateVehicleTypeRequest, Result<Guid>>
{
    private readonly IRepository<VehicleType> _vehicleTypeRepository;
    private readonly IRepository<Branch> _branchRepository;

    public UpdateVehicleTypeRequestHandler(IRepository<VehicleType> vehicleTypeRepository, IRepository<Branch> branchRepository)
    {
        _vehicleTypeRepository = vehicleTypeRepository;
        _branchRepository = branchRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateVehicleTypeRequest request, CancellationToken cancellationToken)
    {
        var vehicleType = await _vehicleTypeRepository.GetByIdAsync(request.Id);

        if (vehicleType == null)
        {
            throw new NotFoundException("VehicleType not found.");
        }

        var branchExists = await _branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
        if (branchExists == null)
        {
            throw new NotFoundException("Provided BranchId does not match any existing branch.");
        }

        vehicleType.Update(
            request.Name,
            request.Type,
            request.TinhTienVeThang,
            request.DonGiaThang,
            request.PhiCapLoaiThe,
            request.PhiLamMoiThe,
            request.DonGiaLuot,
            request.DonGiaBlock,
            request.ThoiGianMienPhi,
            request.ThoiGianDongGia,
            request.ThoiGianBlock,
            request.LoaiTinhTienVeLuot,
            request.BranchId,
            request.Description
        );

        await _vehicleTypeRepository.UpdateAsync(vehicleType);

        return Result<Guid>.Success(request.Id);
    }
}
