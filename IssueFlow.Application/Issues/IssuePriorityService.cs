using IssueFlow.Application.Issues.Dtos;
using IssueFlow.Application.Repositories;

namespace IssueFlow.Application.Issues;

public class IssuePriorityService : IIssuePriorityService
{
    private readonly IIssuePriorityRepository _issuePriorityRepository;

    public IssuePriorityService(IIssuePriorityRepository issuePriorityRepository)
    {
        _issuePriorityRepository = issuePriorityRepository;
    }

    public async Task<IReadOnlyList<ReadIssuePriorityDto>> GetAllIssuePrioritiesAsync()
    {
        return await _issuePriorityRepository.ReadAllIssuePrioritiesAsync();
    }

    public async Task<ReadIssuePriorityDto?> GetIssuePriorityAsync(Guid id)
    {
        return await _issuePriorityRepository.ReadIssuePriorityAsync(id);
    }
}
