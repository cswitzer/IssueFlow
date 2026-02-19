using IssueFlow.Api.Controllers;
using IssueFlow.Application.Issues;
using IssueFlow.Application.Issues.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IssueFlow.Api.Tests.Tests;

public class IssuesControllerTests
{
    private readonly Mock<IIssueService> _mockIssueService;
    private readonly IssuesController _controller;

    public IssuesControllerTests()
    {
        _mockIssueService = new Mock<IIssueService>();
        _controller = new IssuesController(_mockIssueService.Object);
    }

    [Fact]
    public async Task GetIssue_WhenIssueExists_ReturnOkWithIssue()
    {
        var issueId = Guid.NewGuid();
        var expectedIssue = new ReadIssueDto
        {
            Id = issueId,
            Title = "Test Issue",
            Description = "This is a test issue.",
            Key = "PROJ-1",
            ProjectId = Guid.NewGuid(),
            IssueTypeId = Guid.NewGuid(),
            IssueStatusId = Guid.NewGuid(),
            IssuePriorityId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _mockIssueService
            .Setup(s => s.GetIssueAsync(issueId))
            .ReturnsAsync(expectedIssue);

        var result = await _controller.GetIssue(issueId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedIssue = Assert.IsType<ReadIssueDto>(okResult.Value);
        Assert.Equal(expectedIssue.Id, returnedIssue.Id);
        Assert.Equal(expectedIssue.Title, returnedIssue.Title);
        _mockIssueService.Verify(s => s.GetIssueAsync(issueId), Times.Once);
    }

    [Fact]
    public async Task GetIssue_WhenIssueDoesNotExist_ReturnsNotFound()
    {
        var issueId = Guid.NewGuid();
        _mockIssueService
            .Setup(s => s.GetIssueAsync(issueId))
            .ReturnsAsync((ReadIssueDto?)null);

        var result = await _controller.GetIssue(issueId);

        Assert.IsType<NotFoundResult>(result);
        _mockIssueService.Verify(_mockIssueService => _mockIssueService.GetIssueAsync(issueId), Times.Once);
    }

    [Fact]
    public async Task GetIssuesByProject_ReturnsOkWithIssuesList()
    {
        var projectId = Guid.NewGuid();
        var expectedIssues = new List<ReadIssueDto>
        {
            new ReadIssueDto
            {
                Id = Guid.NewGuid(),
                Title = "Issue 1",
                Description = "Description 1",
                Key = "PROJ-1",
                ProjectId = projectId,
                IssueTypeId = Guid.NewGuid(),
                IssueStatusId = Guid.NewGuid(),
                IssuePriorityId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            },
            new ReadIssueDto
            {
                Id = Guid.NewGuid(),
                Title = "Issue 2",
                Description = "Description 2",
                Key = "PROJ-2",
                ProjectId = projectId,
                IssueTypeId = Guid.NewGuid(),
                IssueStatusId = Guid.NewGuid(),
                IssuePriorityId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            }
        };

        _mockIssueService
            .Setup(s => s.GetIssuesByProjectAsync(projectId, 1, 15))
            .ReturnsAsync(expectedIssues);

        var result = await _controller.GetIssuesByProject(projectId, 1, 15);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedIssues = Assert.IsType<List<ReadIssueDto>>(okResult.Value);
        Assert.Equal(expectedIssues.Count, returnedIssues.Count);
    }

    [Theory]
    [InlineData(1, 15)]
    [InlineData(2, 25)]
    [InlineData(3, 50)]
    public async Task GetIssuesByProject_WithPaginationParameters_PassesCorrectParameters(int page, int pageSize)
    {
        var projectId = Guid.NewGuid();
        var expectedIssues = new List<ReadIssueDto>();
        _mockIssueService
            .Setup(s => s.GetIssuesByProjectAsync(projectId, page, pageSize))
            .ReturnsAsync(expectedIssues);

        var result = await _controller.GetIssuesByProject(projectId, page, pageSize);

        var okResult = Assert.IsType<OkObjectResult>(result);
        _mockIssueService.Verify(s => s.GetIssuesByProjectAsync(projectId, page, pageSize), Times.Once);
    }

    [Fact]
    public async Task CreateIssue_ReturnsCreatedAtActionWithIssue()
    {
        var createDto = new CreateIssueDto
        {
            ProjectId = Guid.NewGuid(),
            IssueTypeId = Guid.NewGuid(),
            IssueStatusId = Guid.NewGuid(),
            IssuePriorityId = Guid.NewGuid(),
            Title = "New Issue",
            Description = "New Description"
        };

        var createdIssue = new ReadIssueDto
        {
            Id = Guid.NewGuid(),
            Title = createDto.Title,
            Description = createDto.Description,
            Key = "PROJ-1",
            ProjectId = createDto.ProjectId,
            IssueTypeId = createDto.IssueTypeId,
            IssueStatusId = createDto.IssueStatusId,
            IssuePriorityId = createDto.IssuePriorityId,
            CreatedAt = DateTime.UtcNow
        };

        _mockIssueService
            .Setup(s => s.CreateIssueAsync(createDto))
            .ReturnsAsync(createdIssue);

        var result = await _controller.CreateIssue(createDto);
        
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnedIssue = Assert.IsType<ReadIssueDto>(createdAtActionResult.Value);
        Assert.Equal(createdIssue.Id, returnedIssue.Id);
        Assert.Equal(createdIssue.Title, returnedIssue.Title);
        _mockIssueService.Verify(s => s.CreateIssueAsync(createDto), Times.Once);
    }

    [Fact]
    public async Task UpdateIssue_WhenIssueExists_ReturnsOkWithUpdatedIssue()
    {
        var issueId = Guid.NewGuid();
        var updateDto = new UpdateIssueDto
        {
            Title = "Updated Title",
            Description = "Updated Description"
        };

        var updatedIssue = new ReadIssueDto
        {
            Id = issueId,
            Title = updateDto.Title,
            Description = updateDto.Description!,
            Key = "PROJ-1",
            ProjectId = Guid.NewGuid(),
            IssueTypeId = Guid.NewGuid(),
            IssueStatusId = Guid.NewGuid(),
            IssuePriorityId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _mockIssueService
            .Setup(s => s.UpdateIssueAsync(issueId, updateDto))
            .ReturnsAsync(updatedIssue);

        var result = await _controller.UpdateIssue(issueId, updateDto);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedIssue = Assert.IsType<ReadIssueDto>(okResult.Value);
        Assert.Equal(updatedIssue.Title, returnedIssue.Title);
        _mockIssueService.Verify(s => s.UpdateIssueAsync(issueId, updateDto), Times.Once);
    }

    [Fact]
    public async Task UpdateIssue_WhenIssueDoesNotExist_ReturnsNotFound()
    {
        var issueId = Guid.NewGuid();
        var updateDto = new UpdateIssueDto { Title = "Updated Title" };

        _mockIssueService
            .Setup(s => s.UpdateIssueAsync(issueId, updateDto))
            .ReturnsAsync((ReadIssueDto?)null);

        var result = await _controller.UpdateIssue(issueId, updateDto);

        Assert.IsType<NotFoundResult>(result);
        _mockIssueService.Verify(s => s.UpdateIssueAsync(issueId, updateDto), Times.Once);
    }

    [Fact]
    public async Task DeleteIssue_WhenIssueExists_ReturnsOkWithDeletedIssue()
    {
        var issueId = Guid.NewGuid();
        var deletedIssue = new ReadIssueDto
        {
            Id = issueId,
            Title = "Deleted Issue",
            Description = "Deleted Description",
            Key = "PROJ-1",
            ProjectId = Guid.NewGuid(),
            IssueTypeId = Guid.NewGuid(),
            IssueStatusId = Guid.NewGuid(),
            IssuePriorityId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow
        };

        _mockIssueService
            .Setup(s => s.DeleteIssueAsync(issueId))
            .ReturnsAsync(deletedIssue);

        var result = await _controller.DeleteIssue(issueId);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedIssue = Assert.IsType<ReadIssueDto>(okResult.Value);
        Assert.Equal(deletedIssue.Id, returnedIssue.Id);
        _mockIssueService.Verify(s => s.DeleteIssueAsync(issueId), Times.Once);
    }

    [Fact]
    public async Task DeleteIssue_WhenIssueDoesNotExist_ReturnsNotFound()
    {
        var issueId = Guid.NewGuid();
        _mockIssueService
            .Setup(s => s.DeleteIssueAsync(issueId))
            .ReturnsAsync((ReadIssueDto?)null);

        var result = await _controller.DeleteIssue(issueId);

        Assert.IsType<NotFoundResult>(result);
        _mockIssueService.Verify(s => s.DeleteIssueAsync(issueId), Times.Once);
    }
}
