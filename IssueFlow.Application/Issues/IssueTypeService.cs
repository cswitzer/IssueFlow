using IssueFlow.Application.Issues.Dtos;
using IssueFlow.Application.Repositories;

namespace IssueFlow.Application.Issues;

public class IssueTypeService : IIssueTypeService
{
    private readonly IIssueTypeRepository _issueTypeRepository;

    public IssueTypeService(IIssueTypeRepository issueTypeRepository)
    {
        _issueTypeRepository = issueTypeRepository;
    }

    public async Task<IReadOnlyList<ReadIssueTypeDto>> GetAllIssueTypesAsync()
    {
        return await _issueTypeRepository.ReadAllIssueTypesAsync();
    }

    public async Task<ReadIssueTypeDto?> GetIssueTypeAsync(Guid id)
    {
        return await _issueTypeRepository.ReadIssueTypeAsync(id);
    }
}
