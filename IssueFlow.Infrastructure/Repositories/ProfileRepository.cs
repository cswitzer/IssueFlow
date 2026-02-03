using IssueFlow.Application.Profiles.Dtos;
using IssueFlow.Application.Repositories;
using IssueFlow.Domain.Joins;
using IssueFlow.Domain.Profiles;
using IssueFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssueFlow.Infrastructure.Repositories;

internal class ProfileRepository : IProfileRepository
{
    private readonly IssueFlowDbContext _dbContext;

    public ProfileRepository(IssueFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReadProfileDto> CreateProfileAsync(CreateProfileDto createProfileDto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var profile = new Profile
            {
                UserId = createProfileDto.UserId,
                FirstName = createProfileDto.FirstName,
                LastName = createProfileDto.LastName,
                ProfilePictureUrl = createProfileDto.ProfilePictureUrl
            };

            _dbContext.Profiles.Add(profile);
            await _dbContext.SaveChangesAsync();

            // Add organization memberships if provided
            if (createProfileDto.OrganizationIds is not null)
            {
                var existingOrgIds = await _dbContext.Organizations
                    .Where(o => createProfileDto.OrganizationIds.Contains(o.Id))
                    .Select(o => o.Id)
                    .ToListAsync();

                var invalidOrgIds = createProfileDto.OrganizationIds.Except(existingOrgIds).ToList();
                if (invalidOrgIds.Any())
                {
                    throw new ArgumentException($"The following organization IDs do not exist: {string.Join(", ", invalidOrgIds)}");
                }

                foreach (var orgId in createProfileDto.OrganizationIds)
                {
                    await _dbContext.OrganizationMembers.AddAsync(new OrganizationMember
                    {
                        ProfileId = profile.Id,
                        OrganizationId = orgId
                    });
                }
                await _dbContext.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            return new ReadProfileDto
            {
                Id = profile.Id,
                DisplayName = $"{profile.FirstName} {profile.LastName}",
                ProfilePictureUrl = profile.ProfilePictureUrl,
                CreatedAt = profile.CreatedAt
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<ReadProfileDto?> DeleteProfileAsync(Guid id)
    {
        var profile = await _dbContext.Profiles
            .FirstOrDefaultAsync(p => p.Id == id);
        if (profile is null)
            return null;

        _dbContext.Profiles.Remove(profile);
        await _dbContext.SaveChangesAsync();
        return new ReadProfileDto
        {
            Id = profile.Id,
            DisplayName = $"{profile.FirstName} {profile.LastName}",
            ProfilePictureUrl = profile.ProfilePictureUrl,
            CreatedAt = profile.CreatedAt
        };
    }

    public async Task<IReadOnlyList<ReadProfileDto>> ReadAllProfilesAsync(int page = 1, int pageSize = 15)
    {
        return await _dbContext.Profiles
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ReadProfileDto
            {
                Id = p.Id,
                DisplayName = $"{p.FirstName} {p.LastName}",
                ProfilePictureUrl = p.ProfilePictureUrl,
                CreatedAt = p.CreatedAt
            }).ToListAsync();
    }

    public async Task<ReadProfileDto?> ReadProfileAsync(Guid id)
    {
        var profile = await _dbContext.Profiles
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (profile is null)
            return null;

        return new ReadProfileDto
        {
            Id = profile.Id,
            DisplayName = $"{profile.FirstName} {profile.LastName}",
            ProfilePictureUrl = profile.ProfilePictureUrl,
            CreatedAt = profile.CreatedAt
        };
    }

    public async Task<IReadOnlyList<ReadProfileDto>> ReadProfilesByOrganization(Guid organizationId, int page = 1, int pageSize = 15)
    {
        // if any organization members for a profile match the organizationId, include that profile
        return await _dbContext.Profiles
            .Where(p => p.OrganizationMembers.Any(om => om.OrganizationId == organizationId))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new ReadProfileDto
            {
                Id = p.Id,
                DisplayName = $"{p.FirstName} {p.LastName}",
                ProfilePictureUrl = p.ProfilePictureUrl,
                CreatedAt = p.CreatedAt
            }).ToListAsync();
    }

    public async Task<ReadProfileDto?> UpdateProfileAsync(Guid id, UpdateProfileDto updateProfileDto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var profile = await _dbContext.Profiles
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
            if (profile is null)
                return null;

            if (updateProfileDto.FirstName is not null)
                profile.FirstName = updateProfileDto.FirstName;
            if (updateProfileDto.LastName is not null)
                profile.LastName = updateProfileDto.LastName;
            if (updateProfileDto.ProfilePictureUrl is not null)
                profile.ProfilePictureUrl = updateProfileDto.ProfilePictureUrl;

            // Handle organization memberships update
            if (updateProfileDto.OrganizationIds is not null)
            {
                var existingOrgIds = await _dbContext.Organizations
                    .Where(o => updateProfileDto.OrganizationIds.Contains(o.Id))
                    .Select(o => o.Id)
                    .ToListAsync();

                var invalidOrgIds = updateProfileDto.OrganizationIds.Except(existingOrgIds).ToList();
                if (invalidOrgIds.Any())
                {
                    throw new ArgumentException($"The following organization IDs do not exist: {string.Join(", ", invalidOrgIds)}");
                }

                var currentMemberships = await _dbContext.OrganizationMembers
                    .Where(m => m.ProfileId == profile.Id)
                    .ToListAsync();
                var currentOrgIds = currentMemberships.Select(m => m.OrganizationId).ToList();

                var membershipsToRemove = currentMemberships
                    .Where(m => !updateProfileDto.OrganizationIds.Contains(m.OrganizationId))
                    .ToList();
                _dbContext.OrganizationMembers.RemoveRange(membershipsToRemove);

                var orgIdsToAdd = updateProfileDto.OrganizationIds
                    .Where(orgId => !currentOrgIds.Contains(orgId))
                    .ToList();

                foreach (var orgId in orgIdsToAdd)
                {
                    await _dbContext.OrganizationMembers.AddAsync(new OrganizationMember
                    {
                        ProfileId = profile.Id,
                        OrganizationId = orgId
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return new ReadProfileDto
            {
                Id = profile.Id,
                DisplayName = $"{profile.FirstName} {profile.LastName}",
                ProfilePictureUrl = profile.ProfilePictureUrl,
                CreatedAt = profile.CreatedAt
            };
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
