namespace IssueFlow.Application.Organizations.Dtos;

public class UpdateOrganizationDto
{
    public string? Name { get; set; }
    public Guid? OwnerProfileId { get; set; }

    // thought about making this a separate DTO, but that is overkill for now
    public bool? IsActive { get; set; }
}
