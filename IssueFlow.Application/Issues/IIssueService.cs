using IssueFlow.Application.Issues.Dtos;

namespace IssueFlow.Application.Issues;

public interface IIssueService
{
    Task<ReadIssueDto> CreateIssueAsync(CreateIssueDto createIssueDto);
    Task<ReadIssueDto?> GetIssueAsync(Guid id);
    Task<IReadOnlyList<ReadIssueDto>> GetIssuesByProjectAsync(Guid projectId, int page = 1, int pageSize = 15);
    Task<ReadIssueDto?> UpdateIssueAsync(Guid id, UpdateIssueDto updateIssueDto);
    Task<ReadIssueDto?> DeleteIssueAsync(Guid id);
}
