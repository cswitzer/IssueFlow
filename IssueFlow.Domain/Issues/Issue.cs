using IssueFlow.Domain.Base;
using IssueFlow.Domain.Profiles;
using IssueFlow.Domain.Projects;

namespace IssueFlow.Domain.Issues;

public class Issue : Entity
{
    public Guid ProjectId { get; set; }
    public Guid IssueTypeId { get; set; }
    public Guid IssueStatusId { get; set; }
    public Guid IssuePriorityId { get; set; }

    // ADD FLUENT CONFIG AND RUN MIGRATIONS, ADD TO CREATE AND UPDATE DTOs
    public Guid? AssigneeProfileId { get; set; }
    public Guid? ReporterProfileId { get; set; }

    public required string Title { get; set; }
    public string Description { get; set; } = string.Empty;
    public required string Key { get; init; } // Pattern [Project.Key]-[IssueNumber]
    public required int IssueNumber { get; init; } // Sequential number within the project

    // Navigation properties (optional)
    public Project? Project { get; set; }
    public IssueType? IssueType { get; set; }
    public IssueStatus? IssueStatus { get; set; }
    public IssuePriority? IssuePriority { get; set; }
    public Profile? AssigneeProfile { get; set; }
    public Profile? ReporterProfile { get; set; }
}
