namespace TD.WebApi.Application.Identity.Users;

public class CreateUserRequest
{
    public string? FirstName { get; set; } 
    public string? FullName { get; set; }
    public string? LastName { get; set; } 
    public string Email { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string ConfirmPassword { get; set; } = default!;
    public string? PhoneNumber { get; set; }
}