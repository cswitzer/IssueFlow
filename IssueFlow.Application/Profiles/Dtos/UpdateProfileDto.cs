namespace IssueFlow.Application.Profiles.Dtos;

public class UpdateProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<Guid>? OrganizationIds { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
