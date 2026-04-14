using IssueFlow.Api.Controllers;
using IssueFlow.Api.Tests.Utilities;
using IssueFlow.Application.Issues;
using IssueFlow.Application.Issues.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IssueFlow.Api.Tests.Tests;

public class IssuePrioritiesControllerTests
{
    private readonly Mock<IIssuePriorityService> _mockIssuePriorityService;
    private readonly IssuePrioritiesController _controller;

    public IssuePrioritiesControllerTests()
    {
        _mockIssuePriorityService = new Mock<IIssuePriorityService>();
        _controller = new IssuePrioritiesController(_mockIssuePriorityService.Object);
    }

    [Fact]
    public async Task GetIssuePriority_WhenExists_ReturnsOkWithIssuePriority()
    {
        var issuePriorityId = Guid.NewGuid();
        var expectedIssuePriority = new ReadIssuePriorityDto
        {
            Id = issuePriorityId,
            Name = "High",
            Description = "High priority issue",
            SortOrder = 1
        };

        _mockIssuePriorityService
            .Setup(s => s.GetIssuePriorityAsync(issuePriorityId))
            .ReturnsAsync(expectedIssuePriority);

        await ControllerTestHelpers.GetById<IIssuePriorityService, ReadIssuePriorityDto>()
            .WithMockService(_mockIssuePriorityService)
            .WithId(issuePriorityId)
            .WithControllerAction(id => _controller.GetIssuePriority(id))
            .WithExpectedDto(expectedIssuePriority)
            .WithCustomAssertions(
                (expected, actual) => Assert.Equal(expected.Id, actual.Id),
                (expected, actual) => Assert.Equal(expected.Name, actual.Name),
                (expected, actual) => Assert.Equal(expected.Description, actual.Description),
                (expected, actual) => Assert.Equal(expected.SortOrder, actual.SortOrder)
            )
            .AssertReturnsOk();
    }

    [Fact]
    public async Task GetIssuePriority_WhenNotExists_ReturnsNotFound()
    {
        var issuePriorityId = Guid.NewGuid();

        _mockIssuePriorityService
            .Setup(s => s.GetIssuePriorityAsync(issuePriorityId))
            .ReturnsAsync((ReadIssuePriorityDto?)null);

        await ControllerTestHelpers.GetById<IIssuePriorityService, ReadIssuePriorityDto>()
            .WithMockService(_mockIssuePriorityService)
            .WithId(issuePriorityId)
            .WithControllerAction(id => _controller.GetIssuePriority(id))
            .AssertReturnsNotFound();
    }

    [Fact]
    public async Task GetAllIssuePriorities_ReturnsOkWithList()
    {
        var expectedPriorities = new List<ReadIssuePriorityDto>
        {
            new ReadIssuePriorityDto { Id = Guid.NewGuid(), Name = "Low", SortOrder = 1 },
            new ReadIssuePriorityDto { Id = Guid.NewGuid(), Name = "Medium", SortOrder = 2 },
            new ReadIssuePriorityDto { Id = Guid.NewGuid(), Name = "High", SortOrder = 3 }
        };

        _mockIssuePriorityService
            .Setup(s => s.GetAllIssuePrioritiesAsync())
            .ReturnsAsync(expectedPriorities);

        var result = await _controller.GetAllIssuePriorities();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedPriorities = Assert.IsAssignableFrom<IReadOnlyList<ReadIssuePriorityDto>>(okResult.Value);
        Assert.Equal(expectedPriorities.Count, returnedPriorities.Count);
        _mockIssuePriorityService.Verify(s => s.GetAllIssuePrioritiesAsync(), Times.Once);
    }
}
