using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Vehicles;
public class AddVehicleRequest : IRequest<Result<Guid>>
{
    public string Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Owner { get; set; }
    public string? PlateNumber { get; set; }
    public string? VehicleImage { get; set; }
    public string? PlateImage { get; set; }
    public Guid? VehicleTypeId { get; set; }
    public Guid? TicketId { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateStop { get; set; }
    public DateTime? DateExtend { get; set; }
    public Guid? BranchId { get; set; }
    public string? Description { get; set; }
}
public class AddVehicleRequestValidator : AbstractValidator<AddVehicleRequest>
{
    public AddVehicleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.");
    }
}

public class AddVehicleRequestHandler : IRequestHandler<AddVehicleRequest, Result<Guid>>
{
    private readonly IRepository<Vehicle> _vehicleRepository;

    public AddVehicleRequestHandler(IRepository<Vehicle> vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<Result<Guid>> Handle(AddVehicleRequest request, CancellationToken cancellationToken)
    {
        var vehicle = new Vehicle(
            request.Name,
            request.PhoneNumber,
            request.Owner,
            request.PlateNumber,
            request.VehicleImage,
            request.PlateImage,
            request.VehicleTypeId,
            request.TicketId,
            request.DateStart,
            request.DateStop,
            request.DateExtend,
            request.BranchId,
            request.Description
        );

        await _vehicleRepository.AddAsync(vehicle, cancellationToken);
        await _vehicleRepository.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(vehicle.Id);
    }
}
