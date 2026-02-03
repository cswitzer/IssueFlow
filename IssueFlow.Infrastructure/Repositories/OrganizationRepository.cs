using IssueFlow.Application.Organizations.Dtos;
using IssueFlow.Application.Repositories;
using IssueFlow.Domain.Organizations;
using IssueFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssueFlow.Infrastructure.Repositories;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly IssueFlowDbContext _dbContext;
    
    public OrganizationRepository(IssueFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReadOrganizationDto> CreateOrganizationAsync(CreateOrganizationDto createOrganizationDto)
    {
        var organization = new Organization
        {
            Name = createOrganizationDto.Name,
            OwnerProfileId = createOrganizationDto.OwnerProfileId,
            IsActive = true
        };

        var newOrganization = _dbContext.Organizations.Add(organization);
        await _dbContext.SaveChangesAsync();

        return new ReadOrganizationDto
        {
            Id = newOrganization.Entity.Id,
            Name = newOrganization.Entity.Name,
            OwnerProfileId = newOrganization.Entity.OwnerProfileId,
            IsActive = newOrganization.Entity.IsActive
        };
    }

    public async Task<ReadOrganizationDto?> DeleteOrganizationAsync(Guid id)
    {
        var organization = await _dbContext.Organizations.FindAsync(id);
        if (organization is null)
            return null;

        _dbContext.Organizations.Remove(organization);
        await _dbContext.SaveChangesAsync();

        return new ReadOrganizationDto
        {
            Id = organization.Id,
            Name = organization.Name,
            OwnerProfileId = organization.OwnerProfileId,
            IsActive = organization.IsActive
        };
    }
    public async Task<IReadOnlyList<ReadOrganizationDto>> ReadOrganizationsByProfileOwner(Guid profileOwnerId, int page = 1, int pageSize = 15)
    {
        return await _dbContext.Organizations
            .Where(o => o.OrganizationMembers.Any(om => om.ProfileId == profileOwnerId))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new ReadOrganizationDto
            {
                Id = o.Id,
                Name = o.Name,
                OwnerProfileId = o.OwnerProfileId,
                IsActive = o.IsActive
            })
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ReadOrganizationDto>> ReadAllOrganizationsAsync(int page = 1, int pageSize = 15)
    {
        return await _dbContext.Organizations
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new ReadOrganizationDto
            {
                Id = o.Id,
                Name = o.Name,
                OwnerProfileId = o.OwnerProfileId,
                IsActive = o.IsActive
            })
            .ToListAsync();
    }

    public async Task<ReadOrganizationDto?> ReadOrganizationAsync(Guid id)
    {
        var organization = await _dbContext.Organizations.FindAsync(id);
        if (organization is null)
            return null;

        return new ReadOrganizationDto
        {
            Id = organization.Id,
            Name = organization.Name,
            OwnerProfileId = organization.OwnerProfileId,
            IsActive = organization.IsActive
        };
    }

    public async Task<ReadOrganizationDto?> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto updateOrganizationDto)
    {
        var organization = await _dbContext.Organizations.FindAsync(id);
        if (organization is null)
            return null;

        if (updateOrganizationDto.Name is not null)
            organization.Name = updateOrganizationDto.Name;
        if (updateOrganizationDto.OwnerProfileId is not null)
            organization.OwnerProfileId = updateOrganizationDto.OwnerProfileId.Value;
        if (updateOrganizationDto.IsActive is not null)
            organization.IsActive = updateOrganizationDto.IsActive.Value;

        await _dbContext.SaveChangesAsync();
        
        return new ReadOrganizationDto
        {
            Id = organization.Id,
            Name = organization.Name,
            OwnerProfileId = organization.OwnerProfileId,
            IsActive = organization.IsActive
        };
    }
}
