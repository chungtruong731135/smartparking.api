using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.Vehicles;
public class UpdateVehicleRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
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

public class UpdateVehicleRequestHandler : IRequestHandler<UpdateVehicleRequest, Result<Guid>>
{
    private readonly IRepository<Vehicle> _vehicleRepository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IRepository<VehicleType> _vehicleTypeRepository;

    public UpdateVehicleRequestHandler(IRepository<Vehicle> vehicleRepository, IRepository<Branch> branchRepository,
                                       IRepository<Ticket> ticketRepository, IRepository<VehicleType> vehicleTypeRepository)
    {
        _vehicleRepository = vehicleRepository;
        _branchRepository = branchRepository;
        _ticketRepository = ticketRepository;
        _vehicleTypeRepository = vehicleTypeRepository;
    }

    public async Task<Result<Guid>> Handle(UpdateVehicleRequest request, CancellationToken cancellationToken)
    {
        var existingVehicle = await _vehicleRepository.GetByIdAsync(request.Id);

        if (existingVehicle == null)
        {
            throw new NotFoundException("Vehicle not found.");
        }

        var branchExists = await _branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
        if (branchExists == null)
        {
            throw new NotFoundException("Provided BranchId does not match any existing branch.");
        }

        var ticketExists = await _ticketRepository.GetByIdAsync(request.TicketId, cancellationToken);
        if (ticketExists == null)
        {
            throw new NotFoundException("Provided TicketId does not match any existing branch.");
        }

        var vehicleTypeExists = await _vehicleTypeRepository.GetByIdAsync(request.VehicleTypeId, cancellationToken);
        if (vehicleTypeExists == null)
        {
            throw new NotFoundException("Provided VehicleTypeId does not match any existing branch.");
        }

        existingVehicle.Update(
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

        await _vehicleRepository.UpdateAsync(existingVehicle);

        return Result<Guid>.Success(existingVehicle.Id);
    }
}
