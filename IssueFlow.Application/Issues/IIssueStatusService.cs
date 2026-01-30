using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Issues;

public interface IIssueStatusService
{
    Task<ReadIssueStatusDto?> GetIssueStatusAsync(Guid id);
    Task<IReadOnlyList<ReadIssueStatusDto>> GetAllIssueStatusesAsync();
}
