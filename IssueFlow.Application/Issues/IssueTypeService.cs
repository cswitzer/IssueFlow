using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Issues;

public class IssueTypeService : IIssueTypeService
{
    public Task<IReadOnlyList<ReadIssueTypeDto>> GetAllIssueTypesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ReadIssueTypeDto?> GetIssueTypeAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
