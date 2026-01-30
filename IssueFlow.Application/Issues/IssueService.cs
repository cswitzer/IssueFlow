using IssueFlow.Application.Issues.Dtos;
using IssueFlow.Application.Repositories;

namespace IssueFlow.Application.Issues;

public class IssueService : IIssueService
{
    private readonly IIssueRepository _issueRepository;

    public IssueService(IIssueRepository issueRepository)
    {
        _issueRepository = issueRepository;
    }

    public async Task<ReadIssueDto> CreateIssueAsync(CreateIssueDto createIssueDto)
    {
        return await _issueRepository.CreateIssueAsync(createIssueDto);
    }

    public async Task<ReadIssueDto?> DeleteIssueAsync(Guid id)
    {
        return await _issueRepository.DeleteIssueAsync(id);
    }

    public async Task<ReadIssueDto?> GetIssueAsync(Guid id)
    {
        return await _issueRepository.ReadIssueAsync(id);
    }

    public async Task<IReadOnlyList<ReadIssueDto>> GetIssuesByProjectAsync(Guid projectId, int page = 1, int pageSize = 15)
    {
        return await _issueRepository.ReadIssuesByProjectAsync(projectId, page: page, pageSize: pageSize);
    }

    public async Task<ReadIssueDto?> UpdateIssueAsync(Guid id, UpdateIssueDto updateIssueDto)
    {
        return await _issueRepository.UpdateIssueAsync(id, updateIssueDto);
    }
}
