namespace TD.WebApi.Domain.Catalog;

/// <summary>
/// Hoa don thanh toan cua ve thang
/// </summary>
public class VehicleMonthlyInvoice : AuditableEntity, IAggregateRoot
{
    /// <summary>
    /// Loai thanh toan
    /// </summary>
    public int? EventType { get; set; } = 0;
    public DateTime? EventDateTime { get; set; }
    public int? TotalBeforeDiscount { get; set; } = 0;
    public int? Total { get; set; } = 0;
    public int? Discount { get; set; } = 0;
    /// <summary>
    /// Loại thanh toan
    /// 0: Tiền mặt
    /// 1: Chuyển khoản
    /// </summary>
    public int? PaymentMethod { get; set; } = 0;
    public DateTime? DateStart { get; set; }
    public DateTime? DateStop { get; set; }
    public DateTime? DateStopPrevious { get; set; }

    public Guid? BranchId { get; set; }
    public Guid? VehicleId { get; set; }
    public string? Description { get;  set; }

    public virtual Branch? Branch { get; set; }
    public virtual Vehicle? Vehicle { get; set; }

    public VehicleMonthlyInvoice(int? eventType, DateTime? eventDateTime, int? totalBeforeDiscount, int? total, int? discount, int? paymentMethod, DateTime? dateStart, DateTime? dateStop, DateTime? dateStopPrevious, DefaultIdType? branchId, DefaultIdType? vehicleId, string? description)
    {
        EventType = eventType;
        EventDateTime = eventDateTime;
        TotalBeforeDiscount = totalBeforeDiscount;
        Total = total;
        Discount = discount;
        PaymentMethod = paymentMethod;
        DateStart = dateStart;
        DateStop = dateStop;
        DateStopPrevious = dateStopPrevious;
        BranchId = branchId;
        VehicleId = vehicleId;
        Description = description;
    }

    public void Update(
    int? eventType,
    DateTime? eventDateTime,
    int? totalBeforeDiscount,
    int? total,
    int? discount,
    int? paymentMethod,
    DateTime? dateStart,
    DateTime? dateStop,
    DateTime? dateStopPrevious,
    Guid? branchId,
    Guid? vehicleId,
    string? description)
    {
        EventType = eventType;
        EventDateTime = eventDateTime;
        TotalBeforeDiscount = totalBeforeDiscount;
        Total = total;
        Discount = discount;
        PaymentMethod = paymentMethod;
        DateStart = dateStart;
        DateStop = dateStop;
        DateStopPrevious = dateStopPrevious;
        BranchId = branchId;
        VehicleId = vehicleId;
        Description = description;
    }

}