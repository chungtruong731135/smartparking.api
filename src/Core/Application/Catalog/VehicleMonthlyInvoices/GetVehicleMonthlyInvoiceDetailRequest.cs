using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleMonthlyInvoices;
public class GetVehicleMonthlyInvoiceDetailRequest : IRequest<Result<VehicleMonthlyInvoiceDto>>
{
    public Guid Id { get; set; }

    public GetVehicleMonthlyInvoiceDetailRequest(Guid id)
    {
        Id = id;
    }
}

public class GetVehicleMonthlyInvoiceDetailRequestHandler : IRequestHandler<GetVehicleMonthlyInvoiceDetailRequest, Result<VehicleMonthlyInvoiceDto>>
{
    private readonly IRepository<VehicleMonthlyInvoice> _repository;

    public GetVehicleMonthlyInvoiceDetailRequestHandler(IRepository<VehicleMonthlyInvoice> repository)
    {
        _repository = repository;
    }

    public async Task<Result<VehicleMonthlyInvoiceDto>> Handle(GetVehicleMonthlyInvoiceDetailRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _repository.GetByIdAsync(request.Id);
        if (invoice == null)
        {
           throw new NotFoundException("Invoice not found.");
        }

        var dto = new VehicleMonthlyInvoiceDto
        {
            Id = invoice.Id,
            EventType = invoice.EventType,
            EventDateTime = invoice.EventDateTime,
            TotalBeforeDiscount = invoice.TotalBeforeDiscount,
            Total = invoice.Total,
            Discount = invoice.Discount,
            PaymentMethod = invoice.PaymentMethod,
            DateStart = invoice.DateStart,
            DateStop = invoice.DateStop,
            DateStopPrevious = invoice.DateStopPrevious,
            BranchId = invoice.BranchId,
            VehicleId = invoice.VehicleId,
            Description = invoice.Description
        };

        return Result<VehicleMonthlyInvoiceDto>.Success(dto);
    }
}
