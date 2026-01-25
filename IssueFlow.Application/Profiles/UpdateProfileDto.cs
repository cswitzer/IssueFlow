namespace IssueFlow.Application.Profiles;

public class UpdateProfileDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ProfilePictureUrl { get; set; }
}
