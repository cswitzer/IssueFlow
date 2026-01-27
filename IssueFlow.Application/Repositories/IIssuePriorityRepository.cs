using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Repositories;

public interface IIssuePriorityRepository
{
    Task<ReadIssuePriorityDto?> ReadIssuePriorityAsync(Guid id);
    Task<IReadOnlyList<ReadIssuePriorityDto>> ReadAllIssuePrioritiesAsync();
}
