namespace IssueFlow.Application.Profiles.Dtos;

public class ReadProfileDto
{
    public Guid Id { get; set; }
    public string DisplayName { get; set; } = string.Empty;
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
