using IssueFlow.Domain.Base;

namespace IssueFlow.Domain.Issues;

public class IssueType : Entity
{
    public required string Name { get; set; } // story, bug, task, epic
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public int SortOrder { get; set; }
}
