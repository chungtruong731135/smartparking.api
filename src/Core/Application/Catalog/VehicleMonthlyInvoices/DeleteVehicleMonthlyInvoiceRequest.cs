using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleMonthlyInvoices;
public class DeleteVehicleMonthlyInvoiceRequest : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
    public DeleteVehicleMonthlyInvoiceRequest(Guid id)
    {
        Id = id;
    }
}

public class DeleteVehicleMonthlyInvoiceRequestHandler : IRequestHandler<DeleteVehicleMonthlyInvoiceRequest, Result<Guid>>
{
    private readonly IRepository<VehicleMonthlyInvoice> _repository;

    public DeleteVehicleMonthlyInvoiceRequestHandler(IRepository<VehicleMonthlyInvoice> repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(DeleteVehicleMonthlyInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _repository.GetByIdAsync(request.Id);
        if (invoice == null)
        {
            throw new NotFoundException("Invoice not found.");
        }

        await _repository.DeleteAsync(invoice);
        return Result<Guid>.Success(request.Id);
    }
}
