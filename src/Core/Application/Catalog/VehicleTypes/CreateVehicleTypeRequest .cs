using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleTypes;
public class CreateVehicleTypeRequest : IRequest<Result<Guid>>
{
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

public class CreateVehicleTypeRequestValidator : AbstractValidator<CreateVehicleTypeRequest>
{
    public CreateVehicleTypeRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Type).NotEmpty().WithMessage("Type is required.");
        RuleFor(x => x.BranchId).NotEmpty().WithMessage("BranchId is required.");
    }
}

public class CreateVehicleTypeRequestHandler : IRequestHandler<CreateVehicleTypeRequest, Result<Guid>>
{
    private readonly IRepository<VehicleType> _vehicleTypeRepository;
    private readonly IRepository<Branch> _branchRepository;

    public CreateVehicleTypeRequestHandler(IRepository<VehicleType> vehicleTypeRepository, IRepository<Branch> branchRepository)
    {
        _vehicleTypeRepository = vehicleTypeRepository;
        _branchRepository = branchRepository;
    }

    public async Task<Result<Guid>> Handle(CreateVehicleTypeRequest request, CancellationToken cancellationToken)
    {
        if (request.BranchId.HasValue)
        {
            var branchExists = await _branchRepository.GetByIdAsync(request.BranchId.Value, cancellationToken);
            if (branchExists == null)
            {
                throw new NotFoundException("Provided BranchId does not match any existing branch.");
            }
        }

        var vehicleType = new VehicleType(
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

        await _vehicleTypeRepository.AddAsync(vehicleType, cancellationToken);
        await _vehicleTypeRepository.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(vehicleType.Id);
    }
}
