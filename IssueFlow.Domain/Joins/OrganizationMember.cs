using IssueFlow.Domain.Organizations;
using IssueFlow.Domain.Profiles;

namespace IssueFlow.Domain.Joins;

public class OrganizationMember
{
    public Guid OrganizationId { get; set; }
    public Guid ProfileId { get; set; }
    // Navigation Properties
    public Organization Organization { get; set; }
    public Profile Profile { get; set; }
}
