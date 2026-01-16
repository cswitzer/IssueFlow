using Microsoft.AspNetCore.Identity;

namespace IssueFlow.Infrastructure.Identity;

// Not declared in Domain because users have to due with authentication, which is external to
// business logic
public class ApplicationUser : IdentityUser
{
    public DateTime? LastLoginAt { get; set; }
    public string? SecurityQuestion { get; set; }
    public string? SecurityAnswerHash { get; set; }
}
