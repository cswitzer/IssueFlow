using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Issues;

public interface IIssuePriorityService
{
    Task<ReadIssuePriorityDto?> GetIssuePriorityAsync(Guid id);
    Task<IReadOnlyList<ReadIssuePriorityDto>> GetAllIssuePrioritiesAsync();
}
