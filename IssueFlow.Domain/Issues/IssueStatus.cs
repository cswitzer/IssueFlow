using IssueFlow.Domain.Base;

namespace IssueFlow.Domain.Issues;

public class IssueStatus : Entity
{
    public required string Name { get; set; } // To Do, In Progress, Blocked, Done, Closed, Won't Fix
    public string? Description { get; set; }
    public bool IsFinal { get; set; } // For statuses like Done, Closed, etc.
    public int SortOrder { get; set; }
}
