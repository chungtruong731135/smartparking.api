namespace TD.WebApi.Domain.Catalog;

/// <summary>
/// Bãi đỗ xe
/// </summary>
public class Branch : AuditableEntity, IAggregateRoot
{
    public string Name { get;  set; }
    public string? PhoneNumber { get;  set; }
    public string? Email { get;  set; }
    public string? Website { get;  set; }
    public string? Address { get;  set; }
    public string? Logo { get;  set; }
    public string? Description { get;  set; }

    public Branch(string name, string? phoneNumber, string? email, string? website, string? address, string? logo, string? description)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Website = website;
        Address = address;
        Logo = logo;
        Description = description;
    }

    public Branch Update(string? name, string? phoneNumber, string? email, string? website, string? address, string? logo, string? description)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        PhoneNumber = phoneNumber;
        Email = email;
        Website = website;
        Address = address;
        Logo = logo;
        Description = description;
        return this;
    }
}