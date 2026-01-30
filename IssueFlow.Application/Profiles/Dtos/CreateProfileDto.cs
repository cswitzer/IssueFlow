namespace IssueFlow.Application.Profiles.Dtos;

public class CreateProfileDto
{
    public required string UserId { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public List<Guid>? OrganizationIds { get; set; } = new List<Guid>();
    public string? ProfilePictureUrl { get; set; }
}
