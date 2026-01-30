using IssueFlow.Application.Projects.Dtos;

namespace IssueFlow.Application.Projects;

public interface IProjectService
{
    Task<ReadProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto);
    Task<ReadProjectDto?> GetProjectAsync(Guid id);
    Task<IReadOnlyList<ReadProjectDto>> GetProjectsByProfileOwnerAsync(Guid profileOwnerId, int page = 1, int pageSize = 15);
    Task<IReadOnlyList<ReadProjectDto>> GetAllProjectsAsync(int page = 1, int pageSize = 15);
    Task<ReadProjectDto?> UpdateProjectAsync(Guid id, UpdateProjectDto updateProjectDto);
    Task<ReadProjectDto?> DeleteProjectAsync(Guid id);
}
