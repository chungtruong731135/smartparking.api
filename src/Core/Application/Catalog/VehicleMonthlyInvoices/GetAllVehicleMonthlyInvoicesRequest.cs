using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleMonthlyInvoices;
public class GetAllVehicleMonthlyInvoicesRequest : IRequest<Result<List<VehicleMonthlyInvoiceDto>>>
{
}

public class GetAllVehicleMonthlyInvoicesRequestHandler : IRequestHandler<GetAllVehicleMonthlyInvoicesRequest, Result<List<VehicleMonthlyInvoiceDto>>>
{
    private readonly IRepository<VehicleMonthlyInvoice> _repository;

    public GetAllVehicleMonthlyInvoicesRequestHandler(IRepository<VehicleMonthlyInvoice> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<VehicleMonthlyInvoiceDto>>> Handle(GetAllVehicleMonthlyInvoicesRequest request, CancellationToken cancellationToken)
    {
        var invoices = await _repository.ListAsync(cancellationToken);
        var dtos = invoices.Select(invoice => new VehicleMonthlyInvoiceDto
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
        }).ToList();

        return Result<List<VehicleMonthlyInvoiceDto>>.Success(dtos);
    }
}
