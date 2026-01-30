using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Issues;

public class IssuePriorityService : IIssuePriorityService
{
    public Task<IReadOnlyList<ReadIssuePriorityDto>> GetAllIssuePrioritiesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ReadIssuePriorityDto?> GetIssuePriorityAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
