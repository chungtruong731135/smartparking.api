using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD.WebApi.Application.Catalog.VehicleMonthlyInvoices;
public class VehicleMonthlyInvoiceDto : IDto
{
    public Guid Id { get; set; }
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
    public string Description { get; set; }
}

