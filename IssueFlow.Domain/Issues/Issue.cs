using IssueFlow.Domain.Base;
using IssueFlow.Domain.Projects;

namespace IssueFlow.Domain.Issues;

public class Issue : Entity
{
    public Guid ProjectId { get; set; }
    public Guid IssueTypeId { get; set; }
    public Guid IssueStatusId { get; set; }
    public Guid IssuePriorityId { get; set; }

    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public required string Key { get; set; } // Pattern [Project.Key]-[IssueNumber]

    // Navigation properties (optional)
    public Project? Project { get; set; }
    public IssueType? IssueType { get; set; }
    public IssueStatus? IssueStatus { get; set; }
    public IssuePriority? IssuePriority { get; set; }
}
