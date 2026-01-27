namespace IssueFlow.Application.Issues.Dtos;

public class CreateIssueDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required Guid ProjectId { get; set; }
    public required Guid IssueTypeId { get; set; }
    public required Guid IssueStatusId { get; set; }
    public required Guid IssuePriorityId { get; set; }
}
