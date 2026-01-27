using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Repositories;

public interface IIssueTypeRepository
{
    Task<ReadIssueTypeDto?> ReadIssueTypeAsync(Guid id);
    Task<IReadOnlyList<ReadIssueTypeDto>> ReadAllIssueTypesAsync();
}
