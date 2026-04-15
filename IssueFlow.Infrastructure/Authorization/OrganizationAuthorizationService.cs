using IssueFlow.Application.Authorization;
using IssueFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssueFlow.Infrastructure.Authorization;

internal class OrganizationAuthorizationService : IOrganizationAuthorizationService
{
    private readonly IssueFlowDbContext _dbContext;

    public OrganizationAuthorizationService(IssueFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsUserMemberOfOrganizationAsync(string userId, Guid organizationId)
    {
        return await _dbContext.Profiles
            .Where(p => p.UserId == userId)
            .AnyAsync(p => p.OrganizationMembers.Any(om => om.OrganizationId == organizationId));
    }

    public async Task<bool> IsUserMemberOfProjectOrganizationAsync(string userId, Guid projectId)
    {
        var organizationId = await _dbContext.Projects
            .Where(p => p.Id == projectId)
            .Select(p => (Guid?)p.OrganizationId)
            .FirstOrDefaultAsync();

        if (organizationId is null)
            return false;

        return await IsUserMemberOfOrganizationAsync(userId, organizationId.Value);
    }

    public async Task<bool> IsUserMemberOfIssueOrganizationAsync(string userId, Guid issueId)
    {
        var projectId = await _dbContext.Issues
            .Where(i => i.Id == issueId)
            .Select(i => (Guid?)i.ProjectId)
            .FirstOrDefaultAsync();

        if (projectId is null)
            return false;

        return await IsUserMemberOfProjectOrganizationAsync(userId, projectId.Value);
    }
}
