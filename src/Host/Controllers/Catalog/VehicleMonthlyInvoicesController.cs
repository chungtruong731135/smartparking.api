using Microsoft.AspNetCore.Mvc;
using TD.WebApi.Application.Catalog.VehicleMonthlyInvoices;

namespace TD.WebApi.Host.Controllers.Catalog;
public class VehicleMonthlyInvoicesController : VersionedApiController
{
    [HttpPost]
    [OpenApiOperation("Add a new monthly invoice.", "")]
    public Task<Result<Guid>> AddInvoice(AddVehicleMonthlyInvoiceRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [OpenApiOperation("Update a monthly invoice.", "")]
    public Task<Result<Guid>> UpdateInvoice(Guid id, UpdateVehicleMonthlyInvoiceRequest request)
    {
        return Mediator.Send(request);
    }


    [HttpDelete("{id:guid}")]
    [OpenApiOperation("Delete a monthly invoice.", "")]
    public Task<Result<Guid>> DeleteInvoice(Guid id)
    {
        return Mediator.Send(new DeleteVehicleMonthlyInvoiceRequest(id));
    }

    [HttpGet]
    [OpenApiOperation("Get all monthly invoices.", "")]
    public Task<Result<List<VehicleMonthlyInvoiceDto>>> GetAllInvoices()
    {
        return Mediator.Send(new GetAllVehicleMonthlyInvoicesRequest());
    }

    [HttpGet("{id:guid}")]
    [OpenApiOperation("Get details of a monthly invoice.", "")]
    public Task<Result<VehicleMonthlyInvoiceDto>> GetInvoiceDetail(Guid id)
    {
        return Mediator.Send(new GetVehicleMonthlyInvoiceDetailRequest(id));
    }

}
