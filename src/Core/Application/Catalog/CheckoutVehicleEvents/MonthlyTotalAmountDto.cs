using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.CheckoutVehicleEvents;
public class MonthlyTotalAmountDto : IDto
{
    public DateTime? Month { get; set; }
    public decimal? TotalAmount { get; set; }
}
