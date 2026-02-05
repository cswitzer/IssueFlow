namespace IssueFlow.Application.Projects.Dtos;

public class CreateProjectDto
{
    public required Guid OwnerProfileId { get; set; }
    public required Guid OrganizationId { get; set; }
    public required string Name { get; set; }
    public required string Key { get; set; }
    public string? Description { get; set; }
}
