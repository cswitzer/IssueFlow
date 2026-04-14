using IssueFlow.Api.Controllers;
using IssueFlow.Api.Tests.Utilities;
using IssueFlow.Application.Issues;
using IssueFlow.Application.Issues.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IssueFlow.Api.Tests.Tests;

public class IssueTypesControllerTests
{
    private readonly Mock<IIssueTypeService> _mockIssueTypeService;
    private readonly IssueTypesController _controller;

    public IssueTypesControllerTests()
    {
        _mockIssueTypeService = new Mock<IIssueTypeService>();
        _controller = new IssueTypesController(_mockIssueTypeService.Object);
    }

    [Fact]
    public async Task GetIssueType_WhenExists_ReturnsOkWithIssueType()
    {
        var issueTypeId = Guid.NewGuid();
        var expectedIssueType = new ReadIssueTypeDto
        {
            Id = issueTypeId,
            Name = "Bug",
            Description = "A software bug",
            Icon = "bug-icon",
            SortOrder = 1
        };

        _mockIssueTypeService
            .Setup(s => s.GetIssueTypeAsync(issueTypeId))
            .ReturnsAsync(expectedIssueType);

        await ControllerTestHelpers.GetById<IIssueTypeService, ReadIssueTypeDto>()
            .WithMockService(_mockIssueTypeService)
            .WithId(issueTypeId)
            .WithControllerAction(id => _controller.GetIssueType(id))
            .WithExpectedDto(expectedIssueType)
            .WithCustomAssertions(
                (expected, actual) => Assert.Equal(expected.Id, actual.Id),
                (expected, actual) => Assert.Equal(expected.Name, actual.Name),
                (expected, actual) => Assert.Equal(expected.Description, actual.Description),
                (expected, actual) => Assert.Equal(expected.Icon, actual.Icon),
                (expected, actual) => Assert.Equal(expected.SortOrder, actual.SortOrder)
            )
            .AssertReturnsOk();
    }

    [Fact]
    public async Task GetIssueType_WhenNotExists_ReturnsNotFound()
    {
        var issueTypeId = Guid.NewGuid();

        _mockIssueTypeService
            .Setup(s => s.GetIssueTypeAsync(issueTypeId))
            .ReturnsAsync((ReadIssueTypeDto?)null);

        await ControllerTestHelpers.GetById<IIssueTypeService, ReadIssueTypeDto>()
            .WithMockService(_mockIssueTypeService)
            .WithId(issueTypeId)
            .WithControllerAction(id => _controller.GetIssueType(id))
            .AssertReturnsNotFound();
    }

    [Fact]
    public async Task GetAllIssueTypes_ReturnsOkWithList()
    {
        var expectedIssueTypes = new List<ReadIssueTypeDto>
        {
            new ReadIssueTypeDto { Id = Guid.NewGuid(), Name = "Bug", SortOrder = 1 },
            new ReadIssueTypeDto { Id = Guid.NewGuid(), Name = "Feature", SortOrder = 2 },
            new ReadIssueTypeDto { Id = Guid.NewGuid(), Name = "Task", SortOrder = 3 }
        };

        _mockIssueTypeService
            .Setup(s => s.GetAllIssueTypesAsync())
            .ReturnsAsync(expectedIssueTypes);

        var result = await _controller.GetAllIssueTypes();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedIssueTypes = Assert.IsAssignableFrom<IReadOnlyList<ReadIssueTypeDto>>(okResult.Value);
        Assert.Equal(expectedIssueTypes.Count, returnedIssueTypes.Count);
        _mockIssueTypeService.Verify(s => s.GetAllIssueTypesAsync(), Times.Once);
    }
}
