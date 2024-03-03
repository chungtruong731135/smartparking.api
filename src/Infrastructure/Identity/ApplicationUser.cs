using Microsoft.AspNetCore.Identity;

namespace TD.WebApi.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ImageUrl { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public string? Address { get; set; }
    public string? Gender { get; set; }
    public bool IsActive { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public DateTime? CreatedOn { get; set; }
    public string? CreatedBy { get; set; }

    public string? ObjectId { get; set; }
}