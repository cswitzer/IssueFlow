using IssueFlow.Application.Issues.Dtos;
using IssueFlow.Application.Repositories;
using IssueFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssueFlow.Infrastructure.Repositories;

internal class IssueStatusRepository : IIssueStatusRepository
{
    private readonly IssueFlowDbContext _dbContext;

    public IssueStatusRepository(IssueFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ReadIssueStatusDto>> ReadAllIssueStatusesAsync()
    {
        return await _dbContext.IssueStatuses
            .Select(issueStatus => new ReadIssueStatusDto
            {
                Id = issueStatus.Id,
                Name = issueStatus.Name,
                Description = issueStatus.Description,
                IsFinal = issueStatus.IsFinal,
                SortOrder = issueStatus.SortOrder
            }).ToListAsync();
    }

    public async Task<ReadIssueStatusDto?> ReadIssueStatusAsync(Guid id)
    {
        return await _dbContext.IssueStatuses
            .Where(issueStatus => issueStatus.Id == id)
            .Select(issueStatus => new ReadIssueStatusDto
            {
                Id = issueStatus.Id,
                Name = issueStatus.Name,
                Description = issueStatus.Description,
                IsFinal = issueStatus.IsFinal,
                SortOrder = issueStatus.SortOrder
            }).FirstOrDefaultAsync();
    }
}
