namespace IssueFlow.Application.Profiles;

public class ProfileDto
{
    public required string UserId { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
