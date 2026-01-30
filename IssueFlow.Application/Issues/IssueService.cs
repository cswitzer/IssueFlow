using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Issues;

public class IssueService : IIssueService
{
    public Task<ReadIssueDto> CreateIssueAsync(CreateIssueDto createIssueDto)
    {
        throw new NotImplementedException();
    }

    public Task<ReadIssueDto?> DeleteIssueAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ReadIssueDto?> GetIssueAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<ReadIssueDto>> GetIssuesByProjectAsync(Guid projectId, int page = 1, int pageSize = 15)
    {
        throw new NotImplementedException();
    }

    public Task<ReadIssueDto?> UpdateIssueAsync(Guid id, UpdateIssueDto updateIssueDto)
    {
        throw new NotImplementedException();
    }
}
