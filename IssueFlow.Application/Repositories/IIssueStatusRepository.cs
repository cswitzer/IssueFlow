using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Repositories;

public interface IIssueStatusRepository
{
    Task<ReadIssueStatusDto?> ReadIssueStatusAsync(Guid id);
    Task<IReadOnlyList<ReadIssueStatusDto>> ReadAllIssueStatusesAsync();
}
