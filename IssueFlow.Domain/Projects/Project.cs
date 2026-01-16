using IssueFlow.Domain.Base;
using IssueFlow.Domain.Issues;
using IssueFlow.Domain.Profiles;

namespace IssueFlow.Domain.Projects;

public class Project : Entity
{
    public Guid OwnerProfileId { get; set; }

    public required string Name { get; set; }
    public required string Key { get; set; } // VSD, RAS, etc.
    public string? Description { get; set; }
    public bool IsArchived { get; set; }

    // Navigation Properties
    public Profile? OwnerProfile { get; set; }
    public ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
