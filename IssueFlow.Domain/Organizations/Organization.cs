using IssueFlow.Domain.Base;
using IssueFlow.Domain.Profiles;
using IssueFlow.Domain.Projects;

namespace IssueFlow.Domain.Organizations;

public class Organization : Entity
{
    public required string Name { get; set; }
    public required Guid OwnerProfileId { get; set; }

    // Navigation Properties
    public Profile OwnerProfile { get; set; }
    public ICollection<Profile> Members { get; set; } = new List<Profile>();
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
