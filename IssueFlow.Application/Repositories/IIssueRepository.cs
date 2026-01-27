using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Repositories;

public interface IIssueRepository
{
    Task<ReadIssueDto> CreateIssueAsync(CreateIssueDto createIssueDto);
    Task<ReadIssueDto?> ReadIssueAsync(Guid id);
    Task<IReadOnlyList<ReadIssueDto>> ReadIssuesByProjectAsync(Guid projectId, int page = 1, int pageSize = 15);
    Task<ReadIssueDto?> UpdateIssueAsync(Guid id, UpdateIssueDto updateIssueDto);
    Task<ReadIssueDto?> DeleteIssueAsync(Guid id);
}
