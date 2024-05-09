using Microsoft.AspNetCore.Mvc;
using TD.WebApi.Application.Catalog.CheckoutVehicleEvents;
using TD.WebApi.Application.Catalog.EventVehicles;

namespace TD.WebApi.Host.Controllers.Catalog;
public class CheckoutVehicleEventsController : VersionedApiController
{
    [HttpPost("search-checkout-event")]
    [OpenApiOperation("Search event vehicles checkouts.", "")]
    public Task<PaginationResponse<CheckoutVehicleEventDto>> SearchAsync(SearchCheckoutVehicleEventsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPost("calculate-total-amount-by-time")]
    [OpenApiOperation("calculate total amount.", "")]
    public Task<Result<decimal>> CalculateTotalAmount(CalculateTotalAmountRequest request)
    {
        return Mediator.Send(request);
    }
}
