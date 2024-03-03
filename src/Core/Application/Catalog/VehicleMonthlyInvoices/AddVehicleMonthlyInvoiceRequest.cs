using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleMonthlyInvoices;
public class AddVehicleMonthlyInvoiceRequest : IRequest<Result<Guid>>
{
    public int? EventType { get; set; }
    public DateTime? EventDateTime { get; set; }
    public int? TotalBeforeDiscount { get; set; }
    public int? Total { get; set; }
    public int? Discount { get; set; }
    public int? PaymentMethod { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateStop { get; set; }
    public DateTime? DateStopPrevious { get; set; }
    public Guid? BranchId { get; set; }
    public Guid? VehicleId { get; set; }
    public string? Description { get; set; }
}

public class AddVehicleMonthlyInvoiceRequestHandler : IRequestHandler<AddVehicleMonthlyInvoiceRequest, Result<Guid>>
{
    private readonly IRepository<VehicleMonthlyInvoice> _repository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<Vehicle> _vehicleRepository;

    public AddVehicleMonthlyInvoiceRequestHandler(IRepository<VehicleMonthlyInvoice> repository, IRepository<Branch> branchRepository, IRepository<Vehicle> vehicleRepository)
    {
        _repository = repository;
        _branchRepository = branchRepository;
        _vehicleRepository = vehicleRepository;
    }

    public async Task<Result<Guid>> Handle(AddVehicleMonthlyInvoiceRequest request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(request.BranchId);
        if (branch == null) throw new NotFoundException("Branch not found.");

        var vehicle = await _vehicleRepository.GetByIdAsync(request.VehicleId);
        if (vehicle == null) throw new NotFoundException("Vehicle not found.");

        var invoice = new VehicleMonthlyInvoice(request.EventType, request.EventDateTime, request.TotalBeforeDiscount, request.Total, request.Discount, request.PaymentMethod, request.DateStart, request.DateStop, request.DateStopPrevious, request.BranchId, request.VehicleId, request.Description);

        await _repository.AddAsync(invoice);
        return Result<Guid>.Success(invoice.Id);
    }
}
