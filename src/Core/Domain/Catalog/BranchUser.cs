namespace TD.WebApi.Domain.Catalog;

/// <summary>
/// Danh sách người dùng của bãi đỗ xe
/// </summary>
public class BranchUser : AuditableEntity, IAggregateRoot
{
    public Guid? BranchId { get;  set; }
    public Guid? UserId { get;  set; }
    public string? UserName { get; set; }
    public virtual Branch? Branch { get; set; }

    public BranchUser(DefaultIdType? branchId, DefaultIdType? userId, string? userName)
    {
        BranchId = branchId;
        UserId = userId;
        UserName = userName;
    }
}