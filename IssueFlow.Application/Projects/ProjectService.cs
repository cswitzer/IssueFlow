using IssueFlow.Application.Projects.Dtos;
using IssueFlow.Application.Repositories;

namespace IssueFlow.Application.Projects;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ReadProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto)
    {
        return await _projectRepository.CreateProjectAsync(createProjectDto);
    }

    public async Task<ReadProjectDto?> DeleteProjectAsync(Guid id)
    {
        return await _projectRepository.DeleteProjectAsync(id);
    }

    public async Task<IReadOnlyList<ReadProjectDto>> GetAllProjectsAsync(int page = 1, int pageSize = 15)
    {
        return await _projectRepository.ReadAllProjectsAsync(page: page, pageSize: pageSize);
    }
    public async Task<IReadOnlyList<ReadProjectDto>> GetProjectsByProfileOwnerAsync(Guid profileOwnerId, int page = 1, int pageSize = 15)
    {
        return await _projectRepository.ReadProjectsByProfileOwner(profileOwnerId: profileOwnerId, page: page, pageSize: pageSize);
    }

    public async Task<ReadProjectDto?> GetProjectAsync(Guid id)
    {
        return await _projectRepository.ReadProjectAsync(id);
    }

    public async Task<ReadProjectDto?> UpdateProjectAsync(Guid id, UpdateProjectDto updateProjectDto)
    {
        return await _projectRepository.UpdateProjectAsync(id, updateProjectDto);
    }
}
