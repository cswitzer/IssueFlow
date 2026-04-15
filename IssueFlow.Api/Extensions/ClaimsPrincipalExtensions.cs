using System.Security.Claims;

namespace IssueFlow.Api.Extensions;

internal static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Returns the Identity UserId from the JWT "sub" claim, or null if not present.
    /// </summary>
    public static string? GetUserId(this ClaimsPrincipal principal)
    {
        // ASP.NET Core JWT maps "sub" to ClaimTypes.NameIdentifier by default
        return principal.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? principal.FindFirstValue("sub");
    }
}

