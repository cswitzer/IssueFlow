using IssueFlow.Application.Issues.Dtos;
using IssueFlow.Application.Repositories;
using IssueFlow.Domain.Issues;
using IssueFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace IssueFlow.Infrastructure.Repositories;

internal class IssueRepository : IIssueRepository
{
    private readonly IssueFlowDbContext _dbContext;

    public IssueRepository(IssueFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ReadIssueDto> CreateIssueAsync(CreateIssueDto createIssueDto)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Serializable);

        var project = await _dbContext.Projects
            .Where(p => p.Id == createIssueDto.ProjectId)
            .FirstOrDefaultAsync();
        if (project is null)
            throw new InvalidOperationException("Project with the given ID does not exist.");

        int maxCount = await _dbContext.Issues
            .Where(i => i.ProjectId == createIssueDto.ProjectId)
            .MaxAsync(i => (int?)i.IssueNumber) ?? 0;
        int newIssueNumber = maxCount + 1;
        string newIssueKey = $"{project.Key}-{newIssueNumber}";

        var newIssue = new Issue
        {
            ProjectId = createIssueDto.ProjectId,
            IssueTypeId = createIssueDto.IssueTypeId,
            IssueStatusId = createIssueDto.IssueStatusId,
            IssuePriorityId = createIssueDto.IssuePriorityId,
            Title = createIssueDto.Title,
            Description = createIssueDto.Description,
            Key = newIssueKey,
            IssueNumber = newIssueNumber
        };

        _dbContext.Issues.Add(newIssue);
        await _dbContext.SaveChangesAsync();
        await transaction.CommitAsync();

        return new ReadIssueDto
        { 
            Id = newIssue.Id,
            Title = newIssue.Title,
            Description = newIssue.Description,
            Key = newIssue.Key,
            ProjectId = newIssue.ProjectId,
            IssueTypeId = newIssue.IssueTypeId,
            IssueStatusId = newIssue.IssueStatusId,
            IssuePriorityId = newIssue.IssuePriorityId,
            CreatedAt = newIssue.CreatedAt
        };
    }

    public async Task<ReadIssueDto?> DeleteIssueAsync(Guid id)
    {
        var issue = await _dbContext.Issues
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
        if (issue is null)
            return null;

        _dbContext.Issues.Remove(issue);
        await _dbContext.SaveChangesAsync();

        return new ReadIssueDto
        {
            Id = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Key = issue.Key,
            ProjectId = issue.ProjectId,
            IssueTypeId = issue.IssueTypeId,
            IssueStatusId = issue.IssueStatusId,
            IssuePriorityId = issue.IssuePriorityId,
            CreatedAt = issue.CreatedAt
        };
    }

    public async Task<ReadIssueDto?> ReadIssueAsync(Guid id)
    {
        return await _dbContext.Issues
            .Where(i => i.Id == id)
            .Select(issue => new ReadIssueDto
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Key = issue.Key,
                ProjectId = issue.ProjectId,
                IssueTypeId = issue.IssueTypeId,
                IssueStatusId = issue.IssueStatusId,
                IssuePriorityId = issue.IssuePriorityId,
                CreatedAt = issue.CreatedAt
            }).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<ReadIssueDto>> ReadIssuesByProjectAsync(Guid projectId, int page = 1, int pageSize = 15)
    {
        return await _dbContext.Issues
            .Where(i => i.ProjectId == projectId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(i => i.CreatedAt)
            .Select(issue => new ReadIssueDto
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Key = issue.Key,
                ProjectId = issue.ProjectId,
                IssueTypeId = issue.IssueTypeId,
                IssueStatusId = issue.IssueStatusId,
                IssuePriorityId = issue.IssuePriorityId,
                CreatedAt = issue.CreatedAt
            }).ToListAsync();
    }

    public async Task<ReadIssueDto?> UpdateIssueAsync(Guid id, UpdateIssueDto updateIssueDto)
    {
        var issue = await _dbContext.Issues
            .Where(i => i.Id == id)
            .FirstOrDefaultAsync();
        if (issue is null)
            return null;

        if (updateIssueDto.Title is not null)
            issue.Title = updateIssueDto.Title;
        if (updateIssueDto.Description is not null)
            issue.Description = updateIssueDto.Description;
        if (updateIssueDto.IssueTypeId is not null)
            issue.IssueTypeId = updateIssueDto.IssueTypeId.Value;
        if (updateIssueDto.IssueStatusId is not null)
            issue.IssueStatusId = updateIssueDto.IssueStatusId.Value;
        if (updateIssueDto.IssuePriorityId is not null)
            issue.IssuePriorityId = updateIssueDto.IssuePriorityId.Value;

        await _dbContext.SaveChangesAsync();

        return new ReadIssueDto
        {
            Id = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Key = issue.Key,
            ProjectId = issue.ProjectId,
            IssueTypeId = issue.IssueTypeId,
            IssueStatusId = issue.IssueStatusId,
            IssuePriorityId = issue.IssuePriorityId,
            CreatedAt = issue.CreatedAt
        };
    }
}
