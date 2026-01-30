using IssueFlow.Application.Issues.Dtos;
using IssueFlow.Application.Repositories;

namespace IssueFlow.Application.Issues;

public class IssueStatusService : IIssueStatusService
{
    private readonly IIssueStatusRepository _issueStatusRepository;

    public IssueStatusService(IIssueStatusRepository issueStatusRepository)
    {
        _issueStatusRepository = issueStatusRepository;
    }

    public async Task<IReadOnlyList<ReadIssueStatusDto>> GetAllIssueStatusesAsync()
    {
        return await _issueStatusRepository.ReadAllIssueStatusesAsync();
    }

    public async Task<ReadIssueStatusDto?> GetIssueStatusAsync(Guid id)
    {
        return await _issueStatusRepository.ReadIssueStatusAsync(id);
    }
}
