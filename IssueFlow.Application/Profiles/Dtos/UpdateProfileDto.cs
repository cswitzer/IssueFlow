namespace IssueFlow.Application.Profiles.Dtos;

public class UpdateProfileDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
