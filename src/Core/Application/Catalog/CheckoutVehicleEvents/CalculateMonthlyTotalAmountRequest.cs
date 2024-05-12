using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.CheckoutVehicleEvents;
public class CalculateMonthlyTotalAmountRequest : IRequest<Result<List<MonthlyTotalAmountDto>>>
{
    public Guid? BranchId { get; set; }
}

public class CalculateMonthlyTotalAmountRequestHandler : IRequestHandler<CalculateMonthlyTotalAmountRequest, Result<List<MonthlyTotalAmountDto>>>
{
    private readonly IReadRepository<CheckoutVehicleEvent> _repository;

    public CalculateMonthlyTotalAmountRequestHandler(IReadRepository<CheckoutVehicleEvent> repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<MonthlyTotalAmountDto>>> Handle(CalculateMonthlyTotalAmountRequest request, CancellationToken cancellationToken)
    {
        var spec = new CheckoutVehicleEventsMonthlyAmountSpec(request);
        var events = await _repository.ListAsync(spec, cancellationToken);

        // Calculate the range of months
        var currentDate = DateTime.UtcNow;
        var months = Enumerable.Range(0, 5).Select(m => new DateTime(currentDate.Year, currentDate.Month, 1).AddMonths(-m)).OrderByDescending(d => d);

        // Group and calculate totals from events
        var monthlyEventTotals = events
            .GroupBy(e => new { Year = e.CheckoutDate.Year, Month = e.CheckoutDate.Month })
            .Select(g => new MonthlyTotalAmountDto
            {
                Month = new DateTime(g.Key.Year, g.Key.Month, 1),
                TotalAmount = g.Sum(e => e.Amount)
            })
            .ToList();

        // Ensure all months are represented
        var monthlyAmounts = months.Select(m => new MonthlyTotalAmountDto
        {
            Month = m,
            TotalAmount = monthlyEventTotals.FirstOrDefault(me => me.Month == m)?.TotalAmount ?? 0
        }).ToList();

        return Result<List<MonthlyTotalAmountDto>>.Success(monthlyAmounts);
    }
}
