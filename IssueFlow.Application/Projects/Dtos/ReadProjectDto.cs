namespace IssueFlow.Application.Projects.Dtos;

public class ReadProjectDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required Guid OwnerProfileId { get; set; }
    public required Guid OrganizationId { get; set; }
    public required DateTime CreatedAt { get; set; }
}
