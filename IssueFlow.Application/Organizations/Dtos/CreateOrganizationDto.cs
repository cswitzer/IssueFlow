namespace IssueFlow.Application.Organizations.Dtos;

public class CreateOrganizationDto
{
    public required string Name { get; set; }
    public required Guid OwnerProfileId { get; set; }
}
