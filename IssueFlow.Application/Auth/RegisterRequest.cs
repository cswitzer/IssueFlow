namespace IssueFlow.Application.Auth;

public class RegisterRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
    public required string SecurityQuestion { get; set; }
    public required string SecurityAnswer { get; set; }

    // For setting profile information
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<Guid>? OrganizationIds { get; set; } = new List<Guid>();
}
