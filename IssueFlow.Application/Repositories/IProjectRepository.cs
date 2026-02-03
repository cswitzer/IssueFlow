using IssueFlow.Application.Projects.Dtos;

namespace IssueFlow.Application.Repositories;

public interface IProjectRepository
{
    Task<ReadProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto);
    Task<ReadProjectDto?> ReadProjectAsync(Guid id);
    Task<IReadOnlyList<ReadProjectDto>> ReadProjectsByProfileOwner(Guid profileOwnerId, int page = 1, int pageSize = 15);
    Task<IReadOnlyList<ReadProjectDto>> ReadProjectsByOrganization(Guid organizationId, int page = 1, int pageSize = 15);
    Task<IReadOnlyList<ReadProjectDto>> ReadAllProjectsAsync(int page = 1, int pageSize = 15);
    Task<ReadProjectDto?> UpdateProjectAsync(Guid id, UpdateProjectDto updateProjectDto);
    Task<ReadProjectDto?> DeleteProjectAsync(Guid id);
}
