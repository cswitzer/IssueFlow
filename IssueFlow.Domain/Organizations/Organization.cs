using IssueFlow.Domain.Base;
using IssueFlow.Domain.Joins;
using IssueFlow.Domain.Profiles;
using IssueFlow.Domain.Projects;

namespace IssueFlow.Domain.Organizations;

public class Organization : Entity
{
    public required string Name { get; set; }
    public required Guid OwnerProfileId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public Profile OwnerProfile { get; set; }
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<OrganizationMember> Members { get; set; } = new List<OrganizationMember>();
}
