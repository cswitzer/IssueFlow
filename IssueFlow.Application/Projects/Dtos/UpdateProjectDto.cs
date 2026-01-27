namespace IssueFlow.Application.Projects.Dtos;

public class UpdateProjectDto
{
    public Guid? OwnerProfileId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool? IsArchived { get; set; }
}
