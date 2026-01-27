namespace IssueFlow.Application.Issues.Dtos;

public class ReadIssueDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Key { get; set; }
    public Guid ProjectId { get; set; }
    public Guid IssueTypeId { get; set; }
    public Guid IssueStatusId { get; set; }
    public Guid IssuePriorityId { get; set; }
    public DateTime CreatedAt { get; set; }
}
