namespace IssueFlow.Application.Issues.Dtos;

public class ReadIssueTypeDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public int SortOrder { get; set; }
}
