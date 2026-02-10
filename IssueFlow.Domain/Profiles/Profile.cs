using IssueFlow.Domain.Base;
using IssueFlow.Domain.Issues;
using IssueFlow.Domain.Joins;
using IssueFlow.Domain.Organizations;
using IssueFlow.Domain.Projects;

namespace IssueFlow.Domain.Profiles;

public class Profile : Entity
{
    public required string UserId { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ProfilePictureUrl { get; set; }

    // navigation properties
    public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
    public ICollection<OrganizationMember> OrganizationMembers { get; set; } = new List<OrganizationMember>();
    public ICollection<Issue> AssignedIssues { get; set; } = new List<Issue>();
    public ICollection<Issue> ReportedIssues { get; set; } = new List<Issue>();
}
