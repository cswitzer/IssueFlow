using IssueFlow.Application.Issues.Dtos;
using IssueFlow.Application.Repositories;
using IssueFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssueFlow.Infrastructure.Repositories;

internal class IssueTypeRepository : IIssueTypeRepository
{
    private readonly IssueFlowDbContext _dbContext;

    public IssueTypeRepository(IssueFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<ReadIssueTypeDto>> ReadAllIssueTypesAsync()
    {
        return await _dbContext.IssueTypes
            .Select(it => new ReadIssueTypeDto
            {
                Id = it.Id,
                Name = it.Name,
                Description = it.Description,
                Icon = it.Icon,
                SortOrder = it.SortOrder
            }).ToListAsync();
    }

    public async Task<ReadIssueTypeDto?> ReadIssueTypeAsync(Guid id)
    {
        return await _dbContext.IssueTypes
            .Where(it => it.Id == id)
            .Select(it => new ReadIssueTypeDto
            {
                Id = it.Id,
                Name = it.Name,
                Description = it.Description,
                Icon = it.Icon,
                SortOrder = it.SortOrder
            }).FirstOrDefaultAsync();
    }
}
