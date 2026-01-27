namespace IssueFlow.Application.Profiles.Dtos;

public class ProfileDto
{
    public required string UserId { get; init; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
