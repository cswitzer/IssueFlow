namespace IssueFlow.Application.Authorization;

public interface IOrganizationAuthorizationService
{
    /// <summary>
    /// Returns true if the user (identified by their Identity UserId) is a member of the given organization.
    /// </summary>
    Task<bool> IsUserMemberOfOrganizationAsync(string userId, Guid organizationId);

    /// <summary>
    /// Returns true if the user is a member of the organization that owns the given project.
    /// </summary>
    Task<bool> IsUserMemberOfProjectOrganizationAsync(string userId, Guid projectId);

    /// <summary>
    /// Returns true if the user is a member of the organization that owns the project the issue belongs to.
    /// </summary>
    Task<bool> IsUserMemberOfIssueOrganizationAsync(string userId, Guid issueId);
}
