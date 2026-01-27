namespace IssueFlow.Application.Issues.Dtos;

public class ReadIssueStatusDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsFinal { get; set; }
    public int SortOrder { get; set; }
}
