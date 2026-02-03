namespace IssueFlow.Application.Organizations.Dtos;

public class ReadOrganizationDto
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required Guid OwnerProfileId { get; set; }
    public required bool IsActive { get; set; }
}
