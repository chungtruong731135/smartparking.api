using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.CheckoutVehicleEvents;
public class SearchCheckoutVehicleEventsRequest : PaginationFilter, IRequest<PaginationResponse<CheckoutVehicleEventDto>>
{
    public Guid? BranchId { get; set; }
    public string? PlateNumber { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}

public class SearchCheckoutVehicleEventsRequestHandler : IRequestHandler<SearchCheckoutVehicleEventsRequest, PaginationResponse<CheckoutVehicleEventDto>>
{
    private readonly IReadRepository<CheckoutVehicleEvent> _repository;

    public SearchCheckoutVehicleEventsRequestHandler(IReadRepository<CheckoutVehicleEvent> repository)
    {
        _repository = repository;
    }

    public async Task<PaginationResponse<CheckoutVehicleEventDto>> Handle(SearchCheckoutVehicleEventsRequest request, CancellationToken cancellationToken)
    {
        var spec = new CheckoutVehicleEventsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}
