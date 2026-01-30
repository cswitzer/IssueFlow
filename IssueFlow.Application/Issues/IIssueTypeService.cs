using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Issues;

public interface IIssueTypeService
{
    Task<ReadIssueTypeDto?> GetIssueTypeAsync(Guid id);
    Task<IReadOnlyList<ReadIssueTypeDto>> GetAllIssueTypesAsync();
}
