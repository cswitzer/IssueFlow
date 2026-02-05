using IssueFlow.Application.Projects.Dtos;
using IssueFlow.Application.Repositories;
using IssueFlow.Domain.Projects;
using IssueFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssueFlow.Infrastructure.Repositories;

internal class ProjectRepository : IProjectRepository
{
    private readonly IssueFlowDbContext _dbContext;

    public ProjectRepository(IssueFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReadProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto)
    {
        var project = new Project
        {
            OwnerProfileId = createProjectDto.OwnerProfileId,
            OrganizationId = createProjectDto.OrganizationId,
            Name = createProjectDto.Name,
            Key = createProjectDto.Key,
            Description = createProjectDto.Description
        };

        var newProject = await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();

        return new ReadProjectDto
        {
            Id = newProject.Entity.Id,
            Name = newProject.Entity.Name,
            Description = newProject.Entity.Description,
            OwnerProfileId = newProject.Entity.OwnerProfileId,
            OrganizationId = newProject.Entity.OrganizationId,
            CreatedAt = newProject.Entity.CreatedAt
        };
    }

    public async Task<ReadProjectDto?> DeleteProjectAsync(Guid id)
    {
        var project = await _dbContext.Projects.FindAsync(id);
        if (project is null)
            return null;

        _dbContext.Projects.Remove(project);
        await _dbContext.SaveChangesAsync();

        return new ReadProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            OwnerProfileId = project.OwnerProfileId,
            OrganizationId = project.OrganizationId,
            CreatedAt = project.CreatedAt
        };
    }

    public async Task<IReadOnlyList<ReadProjectDto>> ReadAllProjectsAsync(int page = 1, int pageSize = 15)
    {
        return await _dbContext.Projects
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(project => new ReadProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                OwnerProfileId = project.OwnerProfileId,
                OrganizationId = project.OrganizationId,
                CreatedAt = project.CreatedAt
            })
            .ToListAsync();
    }
    public async Task<IReadOnlyList<ReadProjectDto>> ReadProjectsByProfileOwner(Guid profileOwnerId, int page = 1, int pageSize = 15)
    {
        return await _dbContext.Projects
            .Where(project => project.OwnerProfileId == profileOwnerId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(project => new ReadProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                OwnerProfileId = project.OwnerProfileId,
                OrganizationId = project.OrganizationId,
                CreatedAt = project.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<ReadProjectDto?> ReadProjectAsync(Guid id)
    {
        return await _dbContext.Projects
            .Where(project => project.Id == id)
            .Select(project => new ReadProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                OwnerProfileId = project.OwnerProfileId,
                OrganizationId = project.OrganizationId,
                CreatedAt = project.CreatedAt
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ReadProjectDto?> UpdateProjectAsync(Guid id, UpdateProjectDto updateProjectDto)
    {
        var project = await _dbContext.Projects.FindAsync(id);
        if (project is null)
            return null;

        if (updateProjectDto.Name is not null)
            project.Name = updateProjectDto.Name;
        if (updateProjectDto.Description is not null)
            project.Description = updateProjectDto.Description;
        if (updateProjectDto.OwnerProfileId is not null)
            project.OwnerProfileId = updateProjectDto.OwnerProfileId.Value;
        if (updateProjectDto.IsArchived is not null)
            project.IsArchived = updateProjectDto.IsArchived.Value;

        await _dbContext.SaveChangesAsync();

        return new ReadProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            OwnerProfileId = project.OwnerProfileId,
            OrganizationId = project.OrganizationId,
            CreatedAt = project.CreatedAt
        };
    }

    public async Task<IReadOnlyList<ReadProjectDto>> ReadProjectsByOrganization(Guid organizationId, int page = 1, int pageSize = 15)
    {
        return await _dbContext.Projects
            .Where(p => p.OrganizationId == organizationId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ReadProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                OwnerProfileId = p.OwnerProfileId,
                OrganizationId = p.OrganizationId,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }
}
