using IssueFlow.Application.Issues.Dtos;
using IssueFlow.Application.Repositories;
using IssueFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssueFlow.Infrastructure.Repositories;

internal class IssuePriorityRepository : IIssuePriorityRepository
{
    private readonly IssueFlowDbContext _dbContext;

    public IssuePriorityRepository(IssueFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ReadIssuePriorityDto>> ReadAllIssuePrioritiesAsync()
    {
        return await _dbContext.IssuePriorities
            .Select(ip => new ReadIssuePriorityDto
            {
                Id = ip.Id,
                Name = ip.Name,
                Description = ip.Description
            }).ToListAsync();
    }

    public async Task<ReadIssuePriorityDto?> ReadIssuePriorityAsync(Guid id)
    {
        return await _dbContext.IssuePriorities
            .Where(ip => ip.Id == id)
            .Select(ip => new ReadIssuePriorityDto
            {
                Id = ip.Id,
                Name = ip.Name,
                Description = ip.Description
            }).FirstOrDefaultAsync();
    }
}
