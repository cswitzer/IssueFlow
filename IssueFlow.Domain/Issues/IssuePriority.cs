using IssueFlow.Domain.Base;

namespace IssueFlow.Domain.Issues;

public class IssuePriority : Entity
{
    public required string Name { get; set; } // Low, Medium, High
    public string? Description { get; set; }
    public int SortOrder { get; set; }
}
