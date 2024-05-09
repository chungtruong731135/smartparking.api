using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.CheckoutVehicleEvents;
public class CalculateTotalAmountRequest : IRequest<Result<decimal>>
{
    public Guid? BranchId { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class CalculateTotalAmountRequestHandler : IRequestHandler<CalculateTotalAmountRequest, Result<decimal>>
{
    private readonly IReadRepository<CheckoutVehicleEvent> _repository;

    public CalculateTotalAmountRequestHandler(IReadRepository<CheckoutVehicleEvent> repository)
    {
        _repository = repository;
    }

    public async Task<Result<decimal>> Handle(CalculateTotalAmountRequest request, CancellationToken cancellationToken)
    {
        var spec = new CheckoutVehicleEventsAmountSpec(request);
        var events = await _repository.ListAsync(spec, cancellationToken);
        decimal totalAmount = events.Sum(e => e.Amount ?? 0);

        return Result<decimal>.Success(totalAmount);
    }
}
