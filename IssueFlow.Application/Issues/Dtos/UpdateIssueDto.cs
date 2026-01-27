namespace IssueFlow.Application.Issues.Dtos;

public class UpdateIssueDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Guid? IssueTypeId { get; set; }
    public Guid? IssueStatusId { get; set; }
    public Guid? IssuePriorityId { get; set; }
}
