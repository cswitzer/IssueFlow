using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Issues;

public class IssueStatusService : IIssueStatusService
{
    public Task<IReadOnlyList<ReadIssueStatusDto>> GetAllIssueStatusesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ReadIssueStatusDto?> GetIssueStatusAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
